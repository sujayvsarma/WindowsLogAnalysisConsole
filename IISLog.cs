using System;
using System.Data;
using System.IO;

namespace LogAnalysisConsole
{
    internal class IISLog
    {
        private string serverName = null;
        private DataTable entries = null;

        /// <summary>
        /// Initializes the log to "localhost"
        /// </summary>
        public IISLog()
        {
            serverName = "IIS_localhost";
            entries = new DataTable(serverName);
        }

        /// <summary>
        /// Initializes the log to the given servername (name)
        /// </summary>
        /// <param name="name">Name of the server attached to this log</param>
        public IISLog(string name)
        {
            serverName = name;
            entries = new DataTable("IIS_" + serverName);
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

        public void LoadFromFile(string logFilePathName)
        {
            FileStream fs = new FileStream(logFilePathName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader sr = new StreamReader(fs);
            string logFileContent = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            sr = null;
            fs = null;

            string[] logLines = logFileContent.Split(new char[] { '\n' });

            string[] currentSchema = null;

            foreach (string line in logLines)
            {
                if (line.StartsWith("#Fields:"))
                {
                    currentSchema = AppendLogSchema(line);
                }
                else if ((!string.IsNullOrEmpty(line)) && (!line.StartsWith("#")) && (currentSchema != null))
                {
                    string[] lineElements = line.Trim().Split(new char[] { ' ' });

                    if (lineElements.Length > 0)
                    {
                        DataRow dr = Entries.NewRow();
                        for (int i = 0; i < lineElements.Length; i++)
                        {
                            if (i < Entries.Columns.Count)
                            {
                                dr[currentSchema[i]] = lineElements[i];
                            }
                            else
                            {
                                break;
                            }
                        }

                        Entries.Rows.Add(dr);
                    }
                }
            }

            logLines = null;
        }


        private void CreateLogSchema(string[] logFileText)
        {
            const string FieldLineMarker = "#Fields:";

            if (Entries == null)
            {
                Entries = new DataTable(serverName + "_IISLog");
            }

            foreach (string line in logFileText)
            {
                if (line.StartsWith(FieldLineMarker, StringComparison.InvariantCultureIgnoreCase))
                {
                    // field line
                    string[] fieldsList = line.Replace(FieldLineMarker, "").Trim().Split(new char[] { ' ' });
                    foreach (string field in fieldsList)
                    {
                        if (!Entries.Columns.Contains(field))
                        {
                            DataColumn dc = Entries.Columns.Add(field, typeof(string));
                            dc.SetOrdinal(Entries.Columns.Count - 1);
                        }
                    }
                }
            }
        }

        private string[] AppendLogSchema(string fieldLine)
        {
            const string FieldLineMarker = "#Fields:";
            string[] fieldsList = null;

            if (Entries == null)
            {
                Entries = new DataTable(serverName + "_IISLog");
            }

            if (fieldLine.StartsWith(FieldLineMarker, StringComparison.InvariantCultureIgnoreCase))
            {
                // field line
                fieldsList = fieldLine.Replace(FieldLineMarker, "").Trim().Split(new char[] { ' ' });

                foreach (string field in fieldsList)
                {
                    if (!Entries.Columns.Contains(field))
                    {
                        DataColumn dc = Entries.Columns.Add(field, typeof(string));
                        dc.SetOrdinal(Entries.Columns.Count - 1);
                    }
                }
            }

            return fieldsList;
        }

    }
}
