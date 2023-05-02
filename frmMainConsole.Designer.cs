namespace LogAnalysisConsole
{
    partial class frmMainConsole
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.scView = new System.Windows.Forms.SplitContainer();
            this.tbIISLogFilePath = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbFilterEventSourceName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvFilterConditions = new System.Windows.Forms.DataGridView();
            this.FilterName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.FilterCondition = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.FilterValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExportToExcel = new System.Windows.Forms.Button();
            this.btnProcessLogs = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbFilterEventLogName = new System.Windows.Forms.ComboBox();
            this.tbLimitCount = new System.Windows.Forms.TextBox();
            this.cbLimitRows = new System.Windows.Forms.CheckBox();
            this.cbUseEventLog = new System.Windows.Forms.CheckBox();
            this.cbUseIISLog = new System.Windows.Forms.CheckBox();
            this.btnConvertEndDate = new System.Windows.Forms.Button();
            this.btnConvertStartDate = new System.Windows.Forms.Button();
            this.btnRefreshEndDate = new System.Windows.Forms.Button();
            this.btnRefreshStartDate = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbFilterDateTimezone = new System.Windows.Forms.ComboBox();
            this.dtpFilterEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFilterStartDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbServerNames = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tcLogPages = new System.Windows.Forms.TabControl();
            this.btnCollapseExpand = new System.Windows.Forms.Button();
            this.sfdExportLocation = new System.Windows.Forms.SaveFileDialog();
            this.cbOnlyMergedRows = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.scView)).BeginInit();
            this.scView.Panel1.SuspendLayout();
            this.scView.Panel2.SuspendLayout();
            this.scView.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilterConditions)).BeginInit();
            this.SuspendLayout();
            // 
            // scView
            // 
            this.scView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scView.IsSplitterFixed = true;
            this.scView.Location = new System.Drawing.Point(0, 0);
            this.scView.Name = "scView";
            // 
            // scView.Panel1
            // 
            this.scView.Panel1.Controls.Add(this.tbIISLogFilePath);
            this.scView.Panel1.Controls.Add(this.label7);
            this.scView.Panel1.Controls.Add(this.tbFilterEventSourceName);
            this.scView.Panel1.Controls.Add(this.groupBox1);
            this.scView.Panel1.Controls.Add(this.btnExportToExcel);
            this.scView.Panel1.Controls.Add(this.btnProcessLogs);
            this.scView.Panel1.Controls.Add(this.label6);
            this.scView.Panel1.Controls.Add(this.label5);
            this.scView.Panel1.Controls.Add(this.cbFilterEventLogName);
            this.scView.Panel1.Controls.Add(this.tbLimitCount);
            this.scView.Panel1.Controls.Add(this.cbLimitRows);
            this.scView.Panel1.Controls.Add(this.cbOnlyMergedRows);
            this.scView.Panel1.Controls.Add(this.cbUseEventLog);
            this.scView.Panel1.Controls.Add(this.cbUseIISLog);
            this.scView.Panel1.Controls.Add(this.btnConvertEndDate);
            this.scView.Panel1.Controls.Add(this.btnConvertStartDate);
            this.scView.Panel1.Controls.Add(this.btnRefreshEndDate);
            this.scView.Panel1.Controls.Add(this.btnRefreshStartDate);
            this.scView.Panel1.Controls.Add(this.label4);
            this.scView.Panel1.Controls.Add(this.cbFilterDateTimezone);
            this.scView.Panel1.Controls.Add(this.dtpFilterEndDate);
            this.scView.Panel1.Controls.Add(this.dtpFilterStartDate);
            this.scView.Panel1.Controls.Add(this.label3);
            this.scView.Panel1.Controls.Add(this.label2);
            this.scView.Panel1.Controls.Add(this.tbServerNames);
            this.scView.Panel1.Controls.Add(this.label1);
            this.scView.Panel1MinSize = 0;
            // 
            // scView.Panel2
            // 
            this.scView.Panel2.Controls.Add(this.lblStatus);
            this.scView.Panel2.Controls.Add(this.label8);
            this.scView.Panel2.Controls.Add(this.tcLogPages);
            this.scView.Panel2.Controls.Add(this.btnCollapseExpand);
            this.scView.Size = new System.Drawing.Size(1169, 748);
            this.scView.SplitterDistance = 400;
            this.scView.TabIndex = 0;
            this.scView.Resize += new System.EventHandler(this.scView_Resize);
            // 
            // tbIISLogFilePath
            // 
            this.tbIISLogFilePath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tbIISLogFilePath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.tbIISLogFilePath.Location = new System.Drawing.Point(134, 142);
            this.tbIISLogFilePath.Name = "tbIISLogFilePath";
            this.tbIISLogFilePath.Size = new System.Drawing.Size(257, 20);
            this.tbIISLogFilePath.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 145);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 37;
            this.label7.Text = "IIS Log File Path";
            // 
            // tbFilterEventSourceName
            // 
            this.tbFilterEventSourceName.Location = new System.Drawing.Point(129, 361);
            this.tbFilterEventSourceName.Name = "tbFilterEventSourceName";
            this.tbFilterEventSourceName.Size = new System.Drawing.Size(257, 20);
            this.tbFilterEventSourceName.TabIndex = 15;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvFilterConditions);
            this.groupBox1.Location = new System.Drawing.Point(11, 393);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(375, 306);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Conditions for filtering log entries";
            // 
            // dgvFilterConditions
            // 
            this.dgvFilterConditions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFilterConditions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FilterName,
            this.FilterCondition,
            this.FilterValue});
            this.dgvFilterConditions.Location = new System.Drawing.Point(7, 20);
            this.dgvFilterConditions.Name = "dgvFilterConditions";
            this.dgvFilterConditions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFilterConditions.ShowEditingIcon = false;
            this.dgvFilterConditions.Size = new System.Drawing.Size(362, 280);
            this.dgvFilterConditions.TabIndex = 16;
            // 
            // FilterName
            // 
            this.FilterName.HeaderText = "Filter Name";
            this.FilterName.Name = "FilterName";
            this.FilterName.Width = 150;
            // 
            // FilterCondition
            // 
            this.FilterCondition.HeaderText = "Filter Condition";
            this.FilterCondition.Items.AddRange(new object[] {
            "=",
            "<>",
            ">=",
            "<=",
            "In",
            "Not In",
            "Like",
            "Not Like"});
            this.FilterCondition.Name = "FilterCondition";
            this.FilterCondition.Width = 80;
            // 
            // FilterValue
            // 
            this.FilterValue.HeaderText = "Filter Value";
            this.FilterValue.Name = "FilterValue";
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.Location = new System.Drawing.Point(11, 705);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(136, 23);
            this.btnExportToExcel.TabIndex = 20;
            this.btnExportToExcel.Text = "&Export to Excel";
            this.btnExportToExcel.UseVisualStyleBackColor = true;
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // btnProcessLogs
            // 
            this.btnProcessLogs.Location = new System.Drawing.Point(250, 705);
            this.btnProcessLogs.Name = "btnProcessLogs";
            this.btnProcessLogs.Size = new System.Drawing.Size(136, 23);
            this.btnProcessLogs.TabIndex = 17;
            this.btnProcessLogs.Text = "&Process Logs";
            this.btnProcessLogs.UseVisualStyleBackColor = true;
            this.btnProcessLogs.Click += new System.EventHandler(this.btnProcessLogs_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 363);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 13);
            this.label6.TabIndex = 32;
            this.label6.Text = "Filter for Event Source";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 336);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 13);
            this.label5.TabIndex = 30;
            this.label5.Text = "Filter for Event Log";
            // 
            // cbFilterEventLogName
            // 
            this.cbFilterEventLogName.FormattingEnabled = true;
            this.cbFilterEventLogName.Location = new System.Drawing.Point(129, 333);
            this.cbFilterEventLogName.Name = "cbFilterEventLogName";
            this.cbFilterEventLogName.Size = new System.Drawing.Size(257, 21);
            this.cbFilterEventLogName.TabIndex = 14;
            // 
            // tbLimitCount
            // 
            this.tbLimitCount.Location = new System.Drawing.Point(134, 276);
            this.tbLimitCount.Name = "tbLimitCount";
            this.tbLimitCount.Size = new System.Drawing.Size(42, 20);
            this.tbLimitCount.TabIndex = 13;
            this.tbLimitCount.Text = "100";
            this.tbLimitCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbLimitCount.WordWrap = false;
            // 
            // cbLimitRows
            // 
            this.cbLimitRows.AutoSize = true;
            this.cbLimitRows.Checked = true;
            this.cbLimitRows.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLimitRows.Font = new System.Drawing.Font("Calibri", 8F);
            this.cbLimitRows.Location = new System.Drawing.Point(16, 279);
            this.cbLimitRows.Name = "cbLimitRows";
            this.cbLimitRows.Size = new System.Drawing.Size(92, 17);
            this.cbLimitRows.TabIndex = 12;
            this.cbLimitRows.Text = "Limit to latest";
            this.cbLimitRows.UseVisualStyleBackColor = true;
            // 
            // cbUseEventLog
            // 
            this.cbUseEventLog.AutoSize = true;
            this.cbUseEventLog.Checked = true;
            this.cbUseEventLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUseEventLog.Font = new System.Drawing.Font("Calibri", 8F);
            this.cbUseEventLog.Location = new System.Drawing.Point(134, 256);
            this.cbUseEventLog.Name = "cbUseEventLog";
            this.cbUseEventLog.Size = new System.Drawing.Size(107, 17);
            this.cbUseEventLog.TabIndex = 11;
            this.cbUseEventLog.Text = "Include Event Log";
            this.cbUseEventLog.UseVisualStyleBackColor = true;
            // 
            // cbUseIISLog
            // 
            this.cbUseIISLog.AutoSize = true;
            this.cbUseIISLog.Checked = true;
            this.cbUseIISLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUseIISLog.Font = new System.Drawing.Font("Calibri", 8F);
            this.cbUseIISLog.Location = new System.Drawing.Point(16, 256);
            this.cbUseIISLog.Name = "cbUseIISLog";
            this.cbUseIISLog.Size = new System.Drawing.Size(92, 17);
            this.cbUseIISLog.TabIndex = 10;
            this.cbUseIISLog.Text = "Include IIS Log";
            this.cbUseIISLog.UseVisualStyleBackColor = true;
            // 
            // btnConvertEndDate
            // 
            this.btnConvertEndDate.Location = new System.Drawing.Point(347, 225);
            this.btnConvertEndDate.Name = "btnConvertEndDate";
            this.btnConvertEndDate.Size = new System.Drawing.Size(44, 23);
            this.btnConvertEndDate.TabIndex = 9;
            this.btnConvertEndDate.Text = "CNV";
            this.btnConvertEndDate.UseVisualStyleBackColor = true;
            this.btnConvertEndDate.Click += new System.EventHandler(this.btnConvertEndDate_Click);
            // 
            // btnConvertStartDate
            // 
            this.btnConvertStartDate.Location = new System.Drawing.Point(347, 197);
            this.btnConvertStartDate.Name = "btnConvertStartDate";
            this.btnConvertStartDate.Size = new System.Drawing.Size(44, 23);
            this.btnConvertStartDate.TabIndex = 6;
            this.btnConvertStartDate.Text = "CNV";
            this.btnConvertStartDate.UseVisualStyleBackColor = true;
            this.btnConvertStartDate.Click += new System.EventHandler(this.btnConvertStartDate_Click);
            // 
            // btnRefreshEndDate
            // 
            this.btnRefreshEndDate.Location = new System.Drawing.Point(298, 225);
            this.btnRefreshEndDate.Name = "btnRefreshEndDate";
            this.btnRefreshEndDate.Size = new System.Drawing.Size(44, 23);
            this.btnRefreshEndDate.TabIndex = 8;
            this.btnRefreshEndDate.Text = "RFH";
            this.btnRefreshEndDate.UseVisualStyleBackColor = true;
            this.btnRefreshEndDate.Click += new System.EventHandler(this.btnRefreshEndDate_Click);
            // 
            // btnRefreshStartDate
            // 
            this.btnRefreshStartDate.Location = new System.Drawing.Point(298, 196);
            this.btnRefreshStartDate.Name = "btnRefreshStartDate";
            this.btnRefreshStartDate.Size = new System.Drawing.Size(44, 23);
            this.btnRefreshStartDate.TabIndex = 5;
            this.btnRefreshStartDate.Text = "RFH";
            this.btnRefreshStartDate.UseVisualStyleBackColor = true;
            this.btnRefreshStartDate.Click += new System.EventHandler(this.btnRefreshStartDate_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Date time zone";
            // 
            // cbFilterDateTimezone
            // 
            this.cbFilterDateTimezone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterDateTimezone.FormattingEnabled = true;
            this.cbFilterDateTimezone.Location = new System.Drawing.Point(134, 170);
            this.cbFilterDateTimezone.Name = "cbFilterDateTimezone";
            this.cbFilterDateTimezone.Size = new System.Drawing.Size(257, 21);
            this.cbFilterDateTimezone.TabIndex = 3;
            // 
            // dtpFilterEndDate
            // 
            this.dtpFilterEndDate.CustomFormat = "MMM dd, yyyy HH:mm:ss";
            this.dtpFilterEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFilterEndDate.Location = new System.Drawing.Point(134, 222);
            this.dtpFilterEndDate.MinDate = new System.DateTime(2001, 1, 1, 0, 0, 0, 0);
            this.dtpFilterEndDate.Name = "dtpFilterEndDate";
            this.dtpFilterEndDate.Size = new System.Drawing.Size(158, 20);
            this.dtpFilterEndDate.TabIndex = 7;
            // 
            // dtpFilterStartDate
            // 
            this.dtpFilterStartDate.CustomFormat = "MMM dd, yyyy HH:mm:ss";
            this.dtpFilterStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFilterStartDate.Location = new System.Drawing.Point(134, 196);
            this.dtpFilterStartDate.MinDate = new System.DateTime(2001, 1, 1, 0, 0, 0, 0);
            this.dtpFilterStartDate.Name = "dtpFilterStartDate";
            this.dtpFilterStartDate.Size = new System.Drawing.Size(158, 20);
            this.dtpFilterStartDate.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "To date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 196);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "From date";
            // 
            // tbServerNames
            // 
            this.tbServerNames.AcceptsReturn = true;
            this.tbServerNames.Location = new System.Drawing.Point(16, 30);
            this.tbServerNames.Multiline = true;
            this.tbServerNames.Name = "tbServerNames";
            this.tbServerNames.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbServerNames.Size = new System.Drawing.Size(375, 106);
            this.tbServerNames.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Servers";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Location = new System.Drawing.Point(127, 9);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(622, 15);
            this.lblStatus.TabIndex = 21;
            this.lblStatus.Text = "Ready.";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(77, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Status:";
            // 
            // tcLogPages
            // 
            this.tcLogPages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcLogPages.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcLogPages.Location = new System.Drawing.Point(7, 30);
            this.tcLogPages.Name = "tcLogPages";
            this.tcLogPages.SelectedIndex = 0;
            this.tcLogPages.Size = new System.Drawing.Size(755, 706);
            this.tcLogPages.TabIndex = 18;
            // 
            // btnCollapseExpand
            // 
            this.btnCollapseExpand.Location = new System.Drawing.Point(4, 4);
            this.btnCollapseExpand.Name = "btnCollapseExpand";
            this.btnCollapseExpand.Size = new System.Drawing.Size(58, 23);
            this.btnCollapseExpand.TabIndex = 19;
            this.btnCollapseExpand.Text = "<< / >>";
            this.btnCollapseExpand.UseVisualStyleBackColor = true;
            this.btnCollapseExpand.Click += new System.EventHandler(this.btnCollapseExpand_Click);
            // 
            // sfdExportLocation
            // 
            this.sfdExportLocation.DefaultExt = "*.xlsx";
            this.sfdExportLocation.Filter = "Excel Files (*.xlsx)|*.xlsx";
            this.sfdExportLocation.SupportMultiDottedExtensions = true;
            this.sfdExportLocation.Title = "Select the location to export the file to";
            // 
            // cbOnlyMergedRows
            // 
            this.cbOnlyMergedRows.AutoSize = true;
            this.cbOnlyMergedRows.Checked = true;
            this.cbOnlyMergedRows.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOnlyMergedRows.Font = new System.Drawing.Font("Calibri", 8F);
            this.cbOnlyMergedRows.Location = new System.Drawing.Point(250, 256);
            this.cbOnlyMergedRows.Name = "cbOnlyMergedRows";
            this.cbOnlyMergedRows.Size = new System.Drawing.Size(110, 17);
            this.cbOnlyMergedRows.TabIndex = 11;
            this.cbOnlyMergedRows.Text = "Only merged rows";
            this.cbOnlyMergedRows.UseVisualStyleBackColor = true;
            // 
            // frmMainConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1169, 748);
            this.Controls.Add(this.scView);
            this.MinimumSize = new System.Drawing.Size(1185, 730);
            this.Name = "frmMainConsole";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log Analysis Console";
            this.Shown += new System.EventHandler(this.frmMainConsole_Shown);
            this.scView.Panel1.ResumeLayout(false);
            this.scView.Panel1.PerformLayout();
            this.scView.Panel2.ResumeLayout(false);
            this.scView.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scView)).EndInit();
            this.scView.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilterConditions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbServerNames;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFilterStartDate;
        private System.Windows.Forms.DateTimePicker dtpFilterEndDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbFilterDateTimezone;
        private System.Windows.Forms.Button btnRefreshStartDate;
        private System.Windows.Forms.Button btnRefreshEndDate;
        private System.Windows.Forms.Button btnConvertEndDate;
        private System.Windows.Forms.Button btnConvertStartDate;
        private System.Windows.Forms.Button btnCollapseExpand;
        private System.Windows.Forms.TextBox tbLimitCount;
        private System.Windows.Forms.CheckBox cbLimitRows;
        private System.Windows.Forms.CheckBox cbUseEventLog;
        private System.Windows.Forms.CheckBox cbUseIISLog;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnProcessLogs;
        private System.Windows.Forms.Button btnExportToExcel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbFilterEventLogName;
        private System.Windows.Forms.DataGridView dgvFilterConditions;
        private System.Windows.Forms.TextBox tbFilterEventSourceName;
        private System.Windows.Forms.TabControl tcLogPages;
        private System.Windows.Forms.DataGridViewComboBoxColumn FilterName;
        private System.Windows.Forms.DataGridViewComboBoxColumn FilterCondition;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilterValue;
        private System.Windows.Forms.TextBox tbIISLogFilePath;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.SaveFileDialog sfdExportLocation;
        private System.Windows.Forms.CheckBox cbOnlyMergedRows;
    }
}

