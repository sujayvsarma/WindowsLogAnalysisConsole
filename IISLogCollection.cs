using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LogAnalysisConsole
{
    internal class IISLogCollection
    {

        private Dictionary<string, IISLog> logs = null;
        private string tempFolderPath = null;


        public IISLogCollection()
        {
            logs = new Dictionary<string, IISLog>();
            tempFolderPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "LogAnalysisConsole");
            if (!Directory.Exists(tempFolderPath))
            {
                Directory.CreateDirectory(tempFolderPath);
            }
        }

        /// <summary>
        /// Dictionary of logs. Key is the server name
        /// </summary>
        public Dictionary<string, IISLog> Logs
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
        public void LoadServerSingleLog(IISLog log)
        {
            logs.Add(log.ServerName, log);
        }

        /// <summary>
        /// Load logs from given server for the provided website
        /// </summary>
        /// <param name="server">Server name (or FQDN) to retrieve the logs for</param>
        /// <param name="logAbsolutePath">Absolute path on the server for the IIS Logs</param>
        /// <param name="getLogsFromSubdirectories">If set to TRUE, then fetch the logs from subfolders within the logAbsolutePath as well</param>
        /// <param name="startingDate">Starting date for logs</param>
        /// <param name="endingDate">Ending date for logs</param>
        public void LoadServerLogs(string server, string logAbsolutePath, bool getLogsFromSubdirectories, DateTime startingDate, DateTime endingDate)
        {
            IISLog log = new IISLog(server);
            string serverLogFilePath = null;

            try
            {

                serverLogFilePath = string.Format(
                                        "\\\\{0}\\{1}",
                                        server,
                                        logAbsolutePath.Replace(":", "$")
                                    );

                // check if folder exists on server
                if (!Directory.Exists(serverLogFilePath))
                {
                    throw new InvalidOperationException("Specified path does not exist on server: " + server);
                }

                SearchOption srch = SearchOption.TopDirectoryOnly;
                if (getLogsFromSubdirectories)
                {
                    srch = SearchOption.AllDirectories;
                }

                string[] filesList = Directory.GetFiles(serverLogFilePath, "*.log", srch);
                System.Collections.Concurrent.ConcurrentBag<string> parallelFilesList = new System.Collections.Concurrent.ConcurrentBag<string>();

                foreach (string s in filesList)
                {
                    parallelFilesList.Add(s);
                }

                ParallelOptions po = new ParallelOptions();
                if (Environment.Is64BitOperatingSystem)
                {
                    po.MaxDegreeOfParallelism = Environment.ProcessorCount * 2;
                }
                else
                {
                    po.MaxDegreeOfParallelism = Environment.ProcessorCount - 1;
                }


                Parallel.ForEach(
                    parallelFilesList,
                    po,
                    fileName =>
                    {
                        FileInfo fi = new FileInfo(fileName);
                        string thisFileName = Path.Combine(tempFolderPath, server.Replace(".", "-") + "_" + Path.GetFileName(fi.FullName));

                        if (
                                (fi.Length > 0)
                                && (
                                    ((fi.LastWriteTimeUtc >= startingDate) && (fi.LastWriteTimeUtc <= endingDate))
                                    || ((fi.LastWriteTimeUtc >= startingDate) && (fi.LastWriteTimeUtc <= DateTime.UtcNow))
                                    || (File.Exists(thisFileName) && (fi.Length != (new FileInfo(thisFileName)).Length))
                                )
                           )
                        {
                            try
                            {
                                if (File.Exists(thisFileName))
                                {
                                    FileInfo fo = new FileInfo(thisFileName);

                                    if (fi.LastWriteTimeUtc > fo.LastWriteTimeUtc)
                                    {
                                        File.Delete(thisFileName);
                                    }
                                }

                                if (CopyFile(fi.FullName, thisFileName, false, true, fi.LastWriteTimeUtc))
                                {
                                    log.LoadFromFile(thisFileName);
                                }
                            }
                            catch
                            {
                                // EAT
                            }
                        }
                    }
                );

                if ((log != null) && (log.Entries != null) && (log.Entries.Rows.Count > 0))
                {
                    Logs.Add(server, log);
                }
            }
            catch
            {
            }
            finally
            {
            }
        }

        private bool CopyFile(string sourcePath, string destinationPath, bool overwriteIfExists, bool overwriteIfSourceNewer, DateTime setFileTimeAs)
        {
            bool result = false;

            try
            {
                if (File.Exists(destinationPath))
                {
                    if (overwriteIfExists)
                    {
                        File.Delete(destinationPath);
                    }
                    else if (overwriteIfSourceNewer)
                    {
                        if (File.GetLastWriteTimeUtc(sourcePath) > File.GetLastWriteTimeUtc(destinationPath))
                        {
                            File.Delete(destinationPath);
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }


                int BUFFER_SIZE = 1024;
                FileInfo f = new FileInfo(sourcePath);
                long fileSize = f.Length;
                int multiplier = 1;

                while (fileSize >= 1024)
                {
                    fileSize = fileSize / 1024;
                    multiplier++;
                }

                BUFFER_SIZE = (1024 * 10 * multiplier);

                fileSize = f.Length;
                long currentBytes = 0;

                using (FileStream source = File.OpenRead(sourcePath))
                {
                    using (FileStream destination = File.OpenWrite(destinationPath))
                    {
                        byte[] buffer = new byte[BUFFER_SIZE];
                        int count = source.Read(buffer, 0, buffer.Length);
                        while (count > 0)
                        {
                            currentBytes = currentBytes + count;

                            destination.Write(buffer, 0, count);
                            destination.Flush();

                            string status = "Copied: " + Path.GetFileName(destinationPath) + ": " + currentBytes.ToString() + " of " + fileSize.ToString() + " bytes";

                            count = source.Read(buffer, 0, buffer.Length);
                        }

                        destination.Flush();
                        destination.Close();
                    }

                    source.Close();
                }

                if ((setFileTimeAs != null) && (setFileTimeAs != default(DateTime)))
                {
                    File.SetLastWriteTimeUtc(destinationPath, setFileTimeAs);
                }

                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
            }

            return result;
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
                if (f.Name.StartsWith("IISLog."))
                {
                    where.AppendFormat(
                        "([{0}] {1} \'{2}\')",
                            f.Name.Replace("IISLog.", ""),
                            Filter.GetOperatorValue(f.Operator),
                            f.Value
                    );
                }
            }

            string condition = where.ToString().Replace(")(", ") and (");
            if (!string.IsNullOrEmpty(condition))
            {
                foreach (IISLog log in logs.Values)
                {
                    DataTable temp = new DataTable(log.Entries.TableName);
                    foreach (DataColumn dc in log.Entries.Columns)
                    {
                        temp.Columns.Add(dc.ColumnName, dc.DataType);
                    }

                    if (!temp.Columns.Contains("s-computername"))
                    {
                        temp.Columns.Add("s-computername", typeof(string));
                    }

                    DataRow[] requiredRows = log.Entries.Select(condition);

                    foreach (DataRow dr in requiredRows)
                    {
                        DataRow newRow = temp.NewRow();
                        foreach (DataColumn dc in temp.Columns)
                        {
                            newRow[dc] = dr[dc];
                        }

                        if (newRow["s-computername"] == null)
                        {
                            newRow["s-computername"] = log.ServerName;
                        }

                        temp.Rows.Add(newRow);
                    }

                    log.Entries = temp;
                }
            }
        }

        public void ApplyTransformations()
        {
            foreach (IISLog log in Logs.Values)
            {
                DataTable dtLog = log.Entries;

                // add combined date time column
                if (!dtLog.Columns.Contains("date-time"))
                {
                    dtLog.Columns.Add("date-time", typeof(DateTime));
                }

                if (!dtLog.Columns.Contains("date"))
                {
                    dtLog.Columns.Add("date", typeof(string));
                }

                if (!dtLog.Columns.Contains("time"))
                {
                    dtLog.Columns.Add("time", typeof(string));
                }

                string processedFields = ConfigurationManager.AppSettings["ProcessedIISLogFields"];
                string[] pfFormulae = processedFields.Split(new char[] { ';' });
                foreach (string pfItem in pfFormulae)
                {
                    string[] formEquation = pfItem.Split(new char[] { '|' });

                    if (!dtLog.Columns.Contains(formEquation[1]))
                    {
                        if (formEquation.Length > 2)
                        {
                            try
                            {
                                dtLog.Columns.Add(formEquation[1], Type.GetType(formEquation[2]));
                            }
                            catch
                            {
                                // default to String if we cant find the type
                                dtLog.Columns.Add(formEquation[1], typeof(string));
                            }
                        }
                        else
                        {
                            dtLog.Columns.Add(formEquation[1], typeof(string));
                        }
                    }
                }

                foreach (DataRow r in dtLog.Rows)
                {
                    try
                    {
                        if (
                                (r["date"] != System.DBNull.Value)
                                && (r["time"] != System.DBNull.Value)
                            )
                        {
                            string dt = string.Format("{0} {1}", r["date"], r["time"]);
                            r["date-time"] = DateTime.Parse(dt);
                        }

                        string serverNamePart = log.ServerName;
                        foreach (string pfItem in pfFormulae)
                        {
                            string[] formEquation = pfItem.Split(new char[] { '|' });
                            string[] equationParts = formEquation[0].Replace("(", "").Replace(")", "").Split(new char[] { ',' });

                            string calcval = "";
                            foreach (string ep in equationParts)
                            {
                                calcval = calcval + " " + r[ep];
                            }

                            r[formEquation[1]] = calcval.Trim();

                            if (!string.IsNullOrEmpty(calcval.Trim()))
                            {
                                #region Process special fields
                                switch (formEquation[1])
                                {
                                    case "s-computername":
                                        try
                                        {
                                            IPAddress ip = IPAddress.Parse(r[formEquation[1]].ToString());
                                            if (!Helpers.DnsLookups.ContainsKey(ip.ToString()))
                                            {
                                                try
                                                {
                                                    IPHostEntry ipHE = System.Net.Dns.GetHostEntry(ip.ToString());
                                                    if (ipHE.HostName != ip.ToString())
                                                    {
                                                        r[formEquation[1]] = ipHE.HostName;
                                                    }
                                                    else
                                                    {
                                                        r[formEquation[1]] = serverNamePart;
                                                    }
                                                }
                                                catch
                                                {
                                                    r[formEquation[1]] = serverNamePart;
                                                }

                                                try
                                                {
                                                    Helpers.DnsLookups.Add(ip.ToString(), r[formEquation[1]].ToString());
                                                }
                                                catch { }
                                            }
                                            else
                                            {
                                                r[formEquation[1]] = Helpers.DnsLookups[ip.ToString()];
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            //eat
                                        }
                                        break;

                                    case "HTTPStatus":
                                        try
                                        {
                                            r[formEquation[1]] = Enum.GetName(typeof(HttpStatusCode), (HttpStatusCode)Enum.Parse(typeof(HttpStatusCode), r[formEquation[1]].ToString()));
                                        }
                                        catch
                                        {
                                            r[formEquation[1]] = r[formEquation[1]].ToString() + " - Invalid HTTPStatus Code";
                                        }
                                        break;

                                    case "Win32Status":
                                        try
                                        {
                                            r[formEquation[1]] = (new System.ComponentModel.Win32Exception(unchecked(Int32.Parse(r[formEquation[1]].ToString())))).Message;
                                        }
                                        catch
                                        {
                                            r[formEquation[1]] = r[formEquation[1]].ToString() + " - Invalid Win32 Status Code";
                                        }
                                        break;
                                }
                                #endregion
                            }
                        }

                    }
                    catch
                    {
                    }
                }

                dtLog.AcceptChanges();
            }
        }
    }
}
