using System;
using System.Data;
using System.Diagnostics;

namespace LogAnalysisConsole
{
    internal class EventLog
    {
        private string serverName = null;
        private DataTable entries = null;

        /// <summary>
        /// Initializes the log to "localhost"
        /// </summary>
        public EventLog()
        {
            serverName = "localhost";
            entries = new DataTable("EventLog_" + serverName);
        }

        /// <summary>
        /// Initializes the log to the given servername (name)
        /// </summary>
        /// <param name="server">Name of the server attached to this log</param>
        public EventLog(string server)
        {
            serverName = server;
            entries = new DataTable("EventLog_" + serverName);
        }

        /// <summary>
        /// Name of the server from where this log has been retrieved
        /// </summary>
        public string ServerName
        {
            get
            {
                return serverName;
            }
            set
            {
                serverName = value;
            }
        }

        /// <summary>
        /// Table of entries from the log
        /// </summary>
        public DataTable Entries
        {
            get
            {
                return entries;
            }
            set
            {
                entries = value;
            }
        }

        /// <summary>
        /// Clear data and schema
        /// </summary>
        public void Clear()
        {
            Entries = new DataTable(ServerName);
        }

        public void LoadLog(string logName, string logSource, DateTime startDate, DateTime endDate)
        {
            // create log schema
            if (Entries.Columns.Count < 1)
            {
                Entries.Columns.Add("date-time"         , typeof(DateTime));
                Entries.Columns.Add("date"              , typeof(DateTime));
                Entries.Columns.Add("time"              , typeof(string));
                Entries.Columns.Add("s-computername"    , typeof(string));
                Entries.Columns.Add("s-ip"              , typeof(string));
                Entries.Columns.Add("nt-username"       , typeof(string));
                Entries.Columns.Add("el-message"        , typeof(string));
                Entries.Columns.Add("el-eventid"        , typeof(long));
                Entries.Columns.Add("el-type"           , typeof(string));
                Entries.Columns.Add("el-logname"        , typeof(string));
                Entries.Columns.Add("el-logsource"      , typeof(string));

                Entries.AcceptChanges();
            }

            System.Diagnostics.EventLog serverLog = null;

            if (!string.IsNullOrEmpty(logSource))
            {
                serverLog = new System.Diagnostics.EventLog(logName, ServerName, logSource);
            }
            else
            {
                serverLog = new System.Diagnostics.EventLog(logName, ServerName);
            }

            EventLogEntryCollection logEntries = serverLog.Entries;

            foreach (EventLogEntry entry in logEntries)
            {
                if ((entry.TimeGenerated >= startDate) && (entry.TimeGenerated <= endDate))
                {
                    if ((! string.IsNullOrEmpty(logSource)) && (! entry.Source.Equals(logSource)))
                    {
                        continue;
                    }
                    DataRow newRow = Entries.NewRow();

                    newRow["date-time"] = entry.TimeGenerated;
                    newRow["date"] = entry.TimeGenerated.Date;
                    newRow["time"] = entry.TimeGenerated.ToString("HH:mm:ss");

                    newRow["s-computername"] = ServerName;
                    newRow["s-ip"] = null;

                    newRow["nt-username"] = entry.UserName;

                    newRow["el-message"] = entry.Message;
                    newRow["el-eventid"] = entry.InstanceId;
                    newRow["el-type"] = Enum.GetName(typeof(EventLogEntryType), entry.EntryType);
                    newRow["el-logname"] = logName;
                    newRow["el-logsource"] = entry.Source;

                    Entries.Rows.Add(newRow);
                }
            }

            serverLog.Close();
            serverLog = null;
        }
    }
}
