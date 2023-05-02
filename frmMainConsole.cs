using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace LogAnalysisConsole
{
    public partial class frmMainConsole : Form
    {

        private bool flag_DateEventsEnabled = true;
        private IISLogCollection _iisLogs = new IISLogCollection();
        private EventLogCollection _eventLogs = new EventLogCollection();

        public frmMainConsole()
        {
            InitializeComponent();
        }

        private void btnCollapseExpand_Click(object sender, EventArgs e)
        {
            if (scView.Panel1Collapsed)
            {
                scView.Panel1Collapsed = false;
            }
            else
            {
                scView.Panel1Collapsed = true;
            }
        }

        private void frmMainConsole_Shown(object sender, EventArgs e)
        {
            flag_DateEventsEnabled = false;

            cbFilterDateTimezone.DataSource = Timezones.AvailableTimeZones;
            cbFilterDateTimezone.SelectedItem = Timezones.LocalTimeZone;

            dtpFilterStartDate.Value = DateTime.Now.AddHours(-24);
            dtpFilterEndDate.Value = DateTime.Now;

            DataGridViewComboBoxColumn cb = (DataGridViewComboBoxColumn)dgvFilterConditions.Columns["FilterName"];
            string iisLogFieldList = ConfigurationManager.AppSettings["AvailableIISLogFields"];
            string[] iisFields = iisLogFieldList.Split(new char[] { ' ' });

            foreach (string field in iisFields)
            {
                cb.Items.Add("IIS Log > " + field);
            }

            cb.Items.Add("Event Log > date-time");
            cb.Items.Add("Event Log > date");
            cb.Items.Add("Event Log > time");
            cb.Items.Add("Event Log > s-computername");
            cb.Items.Add("Event Log > s-ip");
            cb.Items.Add("Event Log > nt-username");
            cb.Items.Add("Event Log > el-message");
            cb.Items.Add("Event Log > el-eventid");
            cb.Items.Add("Event Log > el-type");
            cb.Items.Add("Event Log > el-logname");
            cb.Items.Add("Event Log > el-logsource");

            cbFilterEventLogName.Items.Add("Application");
            cbFilterEventLogName.Items.Add("Security");
            cbFilterEventLogName.Items.Add("System");
            cbFilterEventLogName.Items.Add("Setup");

            flag_DateEventsEnabled = true;
        }

        private void btnRefreshStartDate_Click(object sender, EventArgs e)
        {
            if (flag_DateEventsEnabled)
            {
                TimeZoneInfo displayZone = (TimeZoneInfo)cbFilterDateTimezone.SelectedItem;
                DateTime currentTime = DateTime.Now;
                DateTime displayTime = TimeZoneInfo.ConvertTime(currentTime, Timezones.LocalTimeZone, displayZone);
                dtpFilterStartDate.Value = displayTime;
            }
        }

        private void btnRefreshEndDate_Click(object sender, EventArgs e)
        {
            if (flag_DateEventsEnabled)
            {
                TimeZoneInfo displayZone = (TimeZoneInfo)cbFilterDateTimezone.SelectedItem;
                DateTime currentTime = DateTime.Now;
                DateTime displayTime = TimeZoneInfo.ConvertTime(currentTime, Timezones.LocalTimeZone, displayZone);
                dtpFilterEndDate.Value = displayTime;
            }
        }

        private void btnConvertStartDate_Click(object sender, EventArgs e)
        {
            if (flag_DateEventsEnabled)
            {
                TimeZoneInfo displayZone = (TimeZoneInfo)cbFilterDateTimezone.SelectedItem;
                DateTime currentTime = dtpFilterStartDate.Value;
                DateTime displayTime = TimeZoneInfo.ConvertTime(currentTime, Timezones.LocalTimeZone, displayZone);
                dtpFilterStartDate.Value = displayTime;
            }
        }

        private void btnConvertEndDate_Click(object sender, EventArgs e)
        {
            if (flag_DateEventsEnabled)
            {
                TimeZoneInfo displayZone = (TimeZoneInfo)cbFilterDateTimezone.SelectedItem;
                DateTime currentTime = dtpFilterEndDate.Value;
                DateTime displayTime = TimeZoneInfo.ConvertTime(currentTime, Timezones.LocalTimeZone, displayZone);
                dtpFilterEndDate.Value = displayTime;
            }
        }

        private string Status
        {
            get
            {
                return lblStatus.Text;
            }
            set
            {
                lblStatus.Text = value + ".";
                Application.DoEvents();
            }
        }

        private void btnProcessLogs_Click(object sender, EventArgs e)
        {
            #region Validations
            if (string.IsNullOrEmpty(tbServerNames.Text.Trim()))
            {
                ShowMessage("Provide at least one server name.");
                return;
            }

            List<string> serverNames = new List<string>();
            string[] enteredNames = tbServerNames.Text.Trim().Split(new char[] { ' ', ',', ';', '\n' });
            foreach (string s in enteredNames)
            {
                string t = s.Trim();
                if (!string.IsNullOrEmpty(t))
                {
                    if (!serverNames.Contains(t))
                    {
                        serverNames.Add(t);
                    }
                }
            }

            DateTime fromDate = dtpFilterStartDate.Value;
            DateTime toDate = dtpFilterEndDate.Value;

            if (fromDate == toDate)
            {
                ShowMessage("From and To dates cannot be the same.");
            }

            if (fromDate > toDate)
            {
                DateTime ex = toDate;
                toDate = fromDate;
                fromDate = ex;

                dtpFilterStartDate.Value = fromDate;
                dtpFilterEndDate.Value = toDate;
            }

            int rowLimit = 0;
            if (!Int32.TryParse(tbLimitCount.Text.Trim(), out rowLimit))
            {
                cbLimitRows.Checked = false;
            }
            else if (rowLimit < 1)
            {
                ShowMessage("[Limit to Latest] value cannot be less than 1");
                return;
            }

            List<Filter> filters = new List<Filter>();
            foreach (DataGridViewRow userCondition in dgvFilterConditions.Rows)
            {
                if (!userCondition.IsNewRow)
                {
                    Filter f = new Filter();
                    f.Name = userCondition.Cells["FilterName"].Value.ToString().Replace(" > ", ".").Replace(" ", "");
                    f.Value = ((userCondition.Cells["FilterValue"].Value == null) ? null : userCondition.Cells["FilterValue"].Value.ToString());
                    f.Operator = Filter.GetOperatorName((string)userCondition.Cells["FilterCondition"].Value);
                    filters.Add(f);
                }
            }

            Status = "Readying for display";
            foreach (TabPage t in tcLogPages.TabPages)
            {
                DataGridView d = (DataGridView)t.Controls[0];
                d.DataSource = null;

                t.Controls.Clear();
            }
            tcLogPages.TabPages.Clear();

            #endregion

            this.UseWaitCursor = true;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                if (cbUseIISLog.Checked)
                {
                    #region IISLog Processing
                    Status = "Processing IIS Logs";
                    foreach (string s in serverNames)
                    {
                        Status = "Loading logs from: " + s;

                        // if we ever add a "recurse folders" checkbox to the UI, change the "false" below to the value of that option.
                        _iisLogs.LoadServerLogs(s, tbIISLogFilePath.Text.Trim(), false, fromDate, toDate);
                    }

                    if (filters.Count > 0)
                    {
                        Status = "Applying log filters";
                        _iisLogs.ApplyFilters(filters);
                    }

                    Status = "Applying transformations";
                    _iisLogs.ApplyTransformations();

                    Status = "Readying display";
                    foreach (string server in _iisLogs.Logs.Keys)
                    {
                        string pageName = "IIS_" + server;

                        tcLogPages.TabPages.Add(pageName, pageName);

                        DataGridView dgIISLogs = new DataGridView();
                        dgIISLogs.Left = 0;
                        dgIISLogs.Top = 0;
                        dgIISLogs.Width = tcLogPages.TabPages[pageName].DisplayRectangle.Width;
                        dgIISLogs.Height = tcLogPages.TabPages[pageName].DisplayRectangle.Height;
                        dgIISLogs.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
                        dgIISLogs.AllowUserToAddRows = false;
                        dgIISLogs.AllowUserToDeleteRows = false;
                        dgIISLogs.AllowUserToResizeColumns = true;
                        dgIISLogs.ReadOnly = true;
                        dgIISLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                        dgIISLogs.DataSource = _iisLogs.Logs[server].Entries;

                        tcLogPages.TabPages[pageName].Controls.Add(dgIISLogs);

                        Application.DoEvents();
                    }
                    #endregion
                }

                if (cbUseEventLog.Checked)
                {
                    #region EventLog Processing
                    Status = "Processing Event Logs";
                    string filterEventLogSource = ((string.IsNullOrEmpty(tbFilterEventSourceName.Text)) ? null : tbFilterEventSourceName.Text.Trim());
                    string filterEventLogNames = cbFilterEventLogName.Text.Trim();
                    if (string.IsNullOrEmpty(filterEventLogNames))
                    {
                        filterEventLogNames = "Application, Security, System, Setup";
                    }

                    string[] selectedLogs = filterEventLogNames.Split(new char[] { ' ', ',', ';' });
                    foreach (string server in serverNames)
                    {
                        Status = "Loading logs from: " + server;
                        foreach (string logName in selectedLogs)
                        {
                            if (cbFilterEventLogName.Items.Contains(logName))
                            {
                                _eventLogs.LoadServerLogs(server, logName, filterEventLogSource, fromDate, toDate);

                                Application.DoEvents();
                            }
                        }
                    }

                    if (filters.Count > 0)
                    {
                        Status = "Applying log filters";
                        _eventLogs.ApplyFilters(filters);
                    }

                    Status = "Applying transformations";
                    _eventLogs.ApplyTransformations();

                    Status = "Readying display";
                    foreach (string server in _eventLogs.Logs.Keys)
                    {
                        string pageName = "EventLog_" + server;

                        tcLogPages.TabPages.Add(pageName, pageName);

                        DataGridView dgEventLogs = new DataGridView();
                        dgEventLogs.Left = 0;
                        dgEventLogs.Top = 0;
                        dgEventLogs.Width = tcLogPages.TabPages[pageName].DisplayRectangle.Width;
                        dgEventLogs.Height = tcLogPages.TabPages[pageName].DisplayRectangle.Height;
                        dgEventLogs.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
                        dgEventLogs.AllowUserToAddRows = false;
                        dgEventLogs.AllowUserToDeleteRows = false;
                        dgEventLogs.AllowUserToResizeColumns = true;
                        dgEventLogs.ReadOnly = true;
                        dgEventLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                        dgEventLogs.DataSource = _eventLogs.Logs[server].Entries;

                        tcLogPages.TabPages[pageName].Controls.Add(dgEventLogs);

                        Application.DoEvents();
                    }
                    #endregion
                }

                if ((cbUseIISLog.Checked) && (cbUseEventLog.Checked))
                {
                    #region Merged Processing

                    foreach (string server in serverNames)
                    {
                        // create merged table
                        DataTable mergedTable = new DataTable();
                        foreach (DataColumn dc in _iisLogs.Logs[server].Entries.Columns)
                        {
                            mergedTable.Columns.Add(dc.ColumnName, dc.DataType);
                        }
                        foreach (DataColumn dc in _eventLogs.Logs[server].Entries.Columns)
                        {
                            if (!mergedTable.Columns.Contains(dc.ColumnName))
                            {
                                mergedTable.Columns.Add(dc.ColumnName, dc.DataType);
                            }
                        }

                        foreach (DataRow dr in _iisLogs.Logs[server].Entries.Rows)
                        {
                            DataRow r = mergedTable.NewRow();
                            foreach (DataColumn dc in _iisLogs.Logs[server].Entries.Columns)
                            {
                                r[dc.ColumnName] = dr[dc.ColumnName];
                            }
                            mergedTable.Rows.Add(r);
                        }

                        bool isNewRow = false;

                        foreach (DataRow dr in _eventLogs.Logs[server].Entries.Rows)
                        {
                            DataRow r = null;
                            DataRow[] matchingIISRows = mergedTable.Select("(([date-time]='" + dr["date-time"] + "') AND ([s-computername]='" + dr["s-computername"] + "'))");
                                
                            if (matchingIISRows.Length > 0)
                            {
                                r = matchingIISRows[0];
                                isNewRow = false;
                            }
                            else if (cbOnlyMergedRows.Checked)
                            {
                                r = mergedTable.NewRow();
                                isNewRow = true;
                            }
                            
                            if (r != null)
                            {
                                foreach (DataColumn dc in _eventLogs.Logs[server].Entries.Columns)
                                {
                                    r[dc.ColumnName] = dr[dc.ColumnName];
                                }
                            }

                            if ((isNewRow) && (r != null))
                            {
                                mergedTable.Rows.Add(r);
                            }
                            else if (r != null)
                            {
                                mergedTable.AcceptChanges();
                            }
                        }

                        string pageName = "Merged_" + server;

                        tcLogPages.TabPages.Add(pageName, pageName);

                        DataGridView dgMergedLogs = new DataGridView();
                        dgMergedLogs.Left = 0;
                        dgMergedLogs.Top = 0;
                        dgMergedLogs.Width = tcLogPages.TabPages[pageName].DisplayRectangle.Width;
                        dgMergedLogs.Height = tcLogPages.TabPages[pageName].DisplayRectangle.Height;
                        dgMergedLogs.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
                        dgMergedLogs.AllowUserToAddRows = false;
                        dgMergedLogs.AllowUserToDeleteRows = false;
                        dgMergedLogs.AllowUserToResizeColumns = true;
                        dgMergedLogs.ReadOnly = true;
                        dgMergedLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                        dgMergedLogs.DataSource = mergedTable;

                        tcLogPages.TabPages[pageName].Controls.Add(dgMergedLogs);

                        Application.DoEvents();
                    }

                    #endregion
                }

                Status = "Ready";
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
                Status = "ERROR: " + ex.Message;
            }
            finally
            {
                this.UseWaitCursor = false;
                this.Cursor = Cursors.Default;
            }
        }

        private void ShowMessage(string message)
        {
            MessageBox.Show(message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void scView_Resize(object sender, EventArgs e)
        {
            scView.SplitterDistance = 400;
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            DataSet exportData = new DataSet("LogsAnalysis");
            List<string> commonLogKeys = new List<string>();

            foreach (string server in _iisLogs.Logs.Keys)
            {
                exportData.Tables.Add(_iisLogs.Logs[server].Entries);
                commonLogKeys.Add(server);
            }

            foreach (string server in _eventLogs.Logs.Keys)
            {
                exportData.Tables.Add(_eventLogs.Logs[server].Entries);
                if (!commonLogKeys.Contains(server))
                {
                    commonLogKeys.Add(server);
                }
            }

            foreach (string server in commonLogKeys)
            {
                if ((_iisLogs.Logs.ContainsKey(server)) && (_eventLogs.Logs.ContainsKey(server)))
                {
                    // create merged table
                    DataTable mergedTable = new DataTable("Merged_" + server);
                    foreach (DataColumn dc in _iisLogs.Logs[server].Entries.Columns)
                    {
                        mergedTable.Columns.Add(dc.ColumnName, dc.DataType);
                    }
                    foreach (DataRow dr in _iisLogs.Logs[server].Entries.Rows)
                    {
                        DataRow r = mergedTable.NewRow();
                        foreach (DataColumn dc in _iisLogs.Logs[server].Entries.Columns)
                        {
                            r[dc.ColumnName] = dr[dc.ColumnName];
                        }
                        mergedTable.Rows.Add(r);
                    }

                    foreach (DataColumn dc in _eventLogs.Logs[server].Entries.Columns)
                    {
                        if (!mergedTable.Columns.Contains(dc.ColumnName))
                        {
                            mergedTable.Columns.Add(dc.ColumnName, dc.DataType);
                        }
                    }
                    foreach (DataRow dr in _eventLogs.Logs[server].Entries.Rows)
                    {
                        DataRow r = mergedTable.NewRow();
                        foreach (DataColumn dc in _eventLogs.Logs[server].Entries.Columns)
                        {
                            r[dc.ColumnName] = dr[dc.ColumnName];
                        }
                        mergedTable.Rows.Add(r);
                    }

                    exportData.Tables.Add(mergedTable);
                }
            }

            if (sfdExportLocation.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                try
                {
                    if (ExportExcel.SaveFile(exportData, sfdExportLocation.FileName))
                    {
                        MessageBox.Show("File exported to \"" + sfdExportLocation.FileName + "\"", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("File could not be exported to \"" + sfdExportLocation.FileName + "\" due to error: " + ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
