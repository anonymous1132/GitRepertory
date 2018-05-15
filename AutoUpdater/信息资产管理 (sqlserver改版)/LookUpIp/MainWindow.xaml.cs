using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LookUpIp.Extention;
using System.Windows.Forms;
using LookUpIp.ViewModel;
using System.Collections.ObjectModel;
using CJComLibrary;
using System.Threading;
using ListViewItem = System.Windows.Controls.ListViewItem;
using Button = System.Windows.Controls.Button;
using System.Data;

namespace LookUpIp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        WindowState ws;
        WindowState ws1;
        NotifyIcon notifyIcon;
        List<IPInfoViewModel> list=new List<IPInfoViewModel>();
        ObservableCollection<IPInfoViewModel> obc = new ObservableCollection<IPInfoViewModel>();
        string sql = "";
        public MainWindow()
        {
            InitializeComponent();
            icon();
            ws1 = WindowState;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.button1.IsEnabled = false;
        //    loading.Visibility = Visibility.Visible;

            if (checkbox1.IsChecked == false)
            {
                switch (combox1.SelectedIndex)
                {
                    case 0:
                        sql = "select * from IP";
                        break;
                    case 1:
                        sql = "select * from IP where IPAddress like '10.147.184.%'";
                        break;
                    case 2:
                        sql = "select * from IP where IPAddress like '10.147.185.%' and ID <383";
                        break;
                    case 3:
                        sql = "select * from IP where IPAddress like '10.147.185.%' and ID >382";
                        break;
                    case 4:
                        sql = "select * from IP where IPAddress like '10.147.186.%'";
                        break;
                    case 5:
                        sql = "select * from IP where IPAddress like  '10.147.187.%'";
                        break;
                    case 6:
                        sql = "select * from IP where IPAddress like  '10.147.188.%'";
                        break;
                    case 7:
                        sql = "select * from IP where IPAddress like '10.147.189.%'";
                        break;
                    case 8:
                        sql = "select * from IP where IPAddress like  '10.147.190.%'";
                        break;
                    case 9:
                        sql = "select * from IP where IPAddress like  '10.147.191.%'";
                        break;
                    default:
                        sql = "";
                        break;
                }
            }
            else
            {
                switch (combox1.SelectedIndex)
                {
                    case 0:
                        sql = "select * from IP where isused=false";
                        break;
                    case 1:
                        sql = "select * from IP where IPAddress like '10.147.184.%' and isused=false";
                        break;
                    case 2:
                        sql = "select * from IP where IPAddress like '10.147.185.%' and ID <383 and isused=false";
                        break;
                    case 3:
                        sql = "select * from IP where IPAddress like '10.147.185.%' and ID >382 and isused=false";
                        break;
                    case 4:
                        sql = "select * from IP where IPAddress like '10.147.186.%' and isused=false";
                        break;
                    case 5:
                        sql = "select * from IP where IPAddress like  '10.147.187.%' and isused=false";
                        break;
                    case 6:
                        sql = "select * from IP where IPAddress like  '10.147.188.%' and isused=false";
                        break;
                    case 7:
                        sql = "select * from IP where IPAddress like '10.147.189.%' and isused=false";
                        break;
                    case 8:
                        sql = "select * from IP where IPAddress like  '10.147.190.%' and isused=false";
                        break;
                    case 9:
                        sql = "select * from IP where IPAddress like  '10.147.191.%' and isused=false";
                        break;
                    default:
                        sql = "";
                        break;
                }
            }
            accessOper aco = new accessOper();
            aco.getSomeData(sql, User.strConnection);
            IList<IPInfoViewModel> ilist = ModelConvertHelper<IPInfoViewModel>.ConvertToModel(aco.dt);
            list = ModelConvertHelper<IPInfoViewModel>.ConvertIListToList(ilist);
            obc = new ObservableCollection<IPInfoViewModel>(list);
            this.button1.IsEnabled = true;
            this.listView_jcxx.ItemsSource = obc;

        }

        private void icon()
        {
            this.notifyIcon = new NotifyIcon();
            this.notifyIcon.BalloonTipText = "启动查询工具";
            this.notifyIcon.Text = "查询工具";
            this.notifyIcon.Icon =new System.Drawing.Icon("feiji.ico");
            this.notifyIcon.Visible = true;
            notifyIcon.MouseDoubleClick += OnNotifyIconDoubleClick;
            this.notifyIcon.ShowBalloonTip(1000);
        }

        private void OnNotifyIconDoubleClick(object sender, EventArgs e)
        {
            this.Show();
            WindowState = ws1;
        }
        private void Window_StateChanged(object sender, EventArgs e)
        {
            ws = WindowState;
            if (ws == WindowState.Minimized)
            { this.Hide(); }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.listView_jcxx.ItemsSource = obc;
        }
        

    

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            listView_jcxx.SelectedItem = ((Button)sender).DataContext;
            IPInfoViewModel ipv = (IPInfoViewModel)listView_jcxx.SelectedItem;
            ipv.isused = false;
            ipv.user = "";
            ipv.LastUsedTime = null;
            sql = "update IP set isused = false, [user] = \"\", LastUsedTime = null where IPAddress = \"" + ipv.IPAddress + "\"";
            accessOper aco = new accessOper();
            aco.getSomeData(sql, User.strConnection);

        }

        private void btn_used_Click(object sender, RoutedEventArgs e)
        {
            listView_jcxx.SelectedItem = ((Button)sender).DataContext;
            IPInfoViewModel ipv = (IPInfoViewModel)listView_jcxx.SelectedItem;
            ipv.isused = true;
            ipv.LastUsedTime = DateTime.Now;
            sql = "update IP set isused = true,  LastUsedTime = #"+ipv.LastUsedTime.ToString()+"# where IPAddress = \"" + ipv.IPAddress + "\"";
            accessOper aco = new accessOper();
            aco.getSomeData(sql, User.strConnection);
        }

        private void btn_write_Click(object sender, RoutedEventArgs e)
        {
            listView_jcxx.SelectedItem = ((Button)sender).DataContext;
            setuserWindow sw = new setuserWindow();
            sw.Owner = this;
            sw.CloseEvent +=new CloseHandler(getUpdateEvent);
            sw.ShowDialog();
        }

        private void getUpdateEvent(string user)
        {
            IPInfoViewModel ipv = (IPInfoViewModel)listView_jcxx.SelectedItem;

            if (ipv.user == user) return;
            sql = "update IP set  [user] = \"" + user + "\" where IPAddress = \"" + ipv.IPAddress + "\"";
            accessOper aco = new accessOper();
            aco.getSomeData(sql, User.strConnection);
            ipv.user = user;

        }

    }
}
