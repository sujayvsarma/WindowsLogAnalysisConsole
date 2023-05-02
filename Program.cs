using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace LogAnalysisConsole
{

    internal static class Helpers
    {
        public static Dictionary<string, string> DnsLookups = new Dictionary<string, string>();
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMainConsole());
        }
    }
}
