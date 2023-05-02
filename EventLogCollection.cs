using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LogAnalysisConsole
{
    internal class EventLogCollection
    {
        private Dictionary<string, LogAnalysisConsole.EventLog> logs = null;

        public EventLogCollection()
        {
            logs = new Dictionary<string, LogAnalysisConsole.EventLog>();
        }

        /// <summary>
        /// Dictionary of logs. Key is the server name
        /// </summary>
        public Dictionary<string, LogAnalysisConsole.EventLog> Logs
        {
            get
            {
                return logs;
            }
            set
            {
                logs = value;
            }
        }

        /// <summary>
        /// Add an IISLog to the collection
        /// </summary>
        /// <param name="log">The log to add</param>
        public void LoadServerSingleLog(LogAnalysisConsole.EventLog log)
        {
            logs.Add(log.ServerName, log);
        }

        /// <summary>
        /// Load logs from given server for the provided website
        /// </summary>
        /// <param name="server">Server name (or FQDN) to retrieve the logs for</param>
        /// <param name="logName">Name of the log to retrieve from (Application, Security, etc)</param>
        /// <param name="logSource">Name of the Event Source in the log to get from (set to NULL to get all Event Sources)</param>
        public void LoadServerLogs(string server, string logName, string logSource, DateTime startDate, DateTime endDate)
        {
            LogAnalysisConsole.EventLog log = new EventLog(server);

            try
            {
                log.LoadLog(logName, logSource, startDate, endDate);
                Logs.Add(server, log);
            }
            catch
            {
            }
            finally
            {
            }
        }

        /// <summary>
        /// Apply the provided filter conditions. Will apply to all the logs in the collection
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        public void ApplyFilters(List<Filter> filters)
        {
            StringBuilder where = new StringBuilder();

            foreach (Filter f in filters)
            {
                if (f.Name.StartsWith("EventLog."))
                {
                    where.AppendFormat(
                        "([{0}] {1} \'{2}\')",
                            f.Name.Replace("EventLog.", ""),
                            Filter.GetOperatorValue(f.Operator),
                            f.Value
                    );
                }
            }

            string condition = where.ToString().Replace(")(", ") and (");
            if (!string.IsNullOrEmpty(condition))
            {
                foreach (LogAnalysisConsole.EventLog log in logs.Values)
                {
                    DataTable temp = new DataTable(log.Entries.TableName);
                    foreach (DataColumn dc in log.Entries.Columns)
                    {
                        temp.Columns.Add(dc.ColumnName, dc.DataType);
                    }

                    DataRow[] requiredRows = log.Entries.Select(condition);

                    foreach (DataRow dr in requiredRows)
                    {
                        DataRow newRow = temp.NewRow();
                        foreach (DataColumn dc in temp.Columns)
                        {
                            newRow[dc.ColumnName] = dr[dc.ColumnName];
                        }
                        temp.Rows.Add(newRow);
                    }

                    log.Entries = temp;
                }
            }
        }

        public void ApplyTransformations()
        {
            //
        }

    }
}
