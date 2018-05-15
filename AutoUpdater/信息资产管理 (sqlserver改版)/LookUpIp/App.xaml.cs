using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace LookUpIp
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {
            LookUpIp.App app = new LookUpIp.App();
            app.InitializeComponent();
            Ezhu.AutoUpdater.Updater.CheckUpdateStatus();
            Window1 win1 = new Window1();
            app.Run(win1);
        }

    }
    public static class User
    {
      //  string database = AppDomain.CurrentDomain.BaseDirectory;
        public static string strConnectiontest = "Provider = Microsoft.ACE.OLEDB.12.0;" + @"Data Source =test.accdb;Jet OLEDB:Database Password=black5408";
        public static string strConnection = "Provider = Microsoft.ACE.OLEDB.12.0;" + "Data Source =" + AppDomain.CurrentDomain.BaseDirectory + "icd.accdb;Jet OLEDB:Database Password=black5408";
    }
}
