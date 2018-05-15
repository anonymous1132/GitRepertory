using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Test
{
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

            Ezhu.AutoUpdater.Updater.CheckUpdateStatus();
           // MessageBox.Show(Ezhu.AutoUpdater.Updater.Instance.CurrentVersion.ToString());
            Application.Run(new Form1());
        }
    }
}
