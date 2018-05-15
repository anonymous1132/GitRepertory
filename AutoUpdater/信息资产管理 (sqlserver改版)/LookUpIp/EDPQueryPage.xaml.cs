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
using LookUpIp.Classess;
using System.Data;

namespace LookUpIp
{
    /// <summary>
    /// EDPQueryPage.xaml 的交互逻辑
    /// </summary>
    public partial class EDPQueryPage : Page
    {
        public EDPQueryPage()
        {
            InitializeComponent();
        }
        string sql = "";
        List<string> liststr=null;
        private void comboBox_queryitem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void button_quickfind_Click(object sender, RoutedEventArgs e)
        {
            this.button_quickfind.IsEnabled = false;
            switch (comboBox_queryitem.SelectedIndex)
            {
                case 0:
                    sql = "select * from view_getbriefdevice ";
                    break;
                case 1:
                    sql = "select * from IP_INSIDE where IPAddress like'%" + comboBox_input.Text + "%'";
                    break;
                case 2:
                    sql = "select * from IP_INSIDE where isused =" + comboBox_input.Text;
                    break;
                case 3:
                    if (comboBox_input.Text == "TRUE") { sql = "select * from IP_INSIDE where assets_NO is not Null"; }
                    else
                    {
                        sql = "select * from IP_INSIDE where assets_NO is Null";
                    }
                    break;
                case 4:
                    sql = "select * from IP_INSIDE where department like'%" + comboBox_input.Text + "%'";
                    break;
                case 5:
                    sql = "select * from IP_INSIDE where master like'%" + comboBox_input.Text + "%'";
                    break;
                case 6:
                    sql = "select * from IP_INSIDE where devicekind like'%" + comboBox_input.Text + "%'";
                    break;
                case 7:
                    sql = "select * from IP_INSIDE where switch like'%" + comboBox_input.Text + "%'";
                    break;
            }

            if (sql != "" && sql != null)
            {
                try
                {
                    EDPSqlOper sqo = new EDPSqlOper();
                    sqo.getSomeDate(sql);
                    DataTable dt = EDPDecrypt.GetDecryptDT(sqo.dt,liststr);
                    this.listView_jcxx.ItemsSource = dt.DefaultView;
                }
                catch (Exception EX) { MessageBox.Show(EX.ToString()); this.listView_jcxx.ItemsSource = null; }

            }
            this.tb_count.Text = "共查询到" + listView_jcxx.Items.Count.ToString() + "条记录！";
            this.button_quickfind.IsEnabled = true;

        }

        private void checkbox_caozuo_Click(object sender, RoutedEventArgs e)
        {
            if (checkbox_caozuo.IsChecked == false)
            {
                listView_jcxx.SelectedItems.Clear();
            }
            else { listView_jcxx.SelectAll(); }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.checkbox_caozuo.IsChecked = false;
        }

        private void listView_jcxx_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (listView_jcxx.SelectedItems.Count > 1)
            { return; }
            if (listView_jcxx.SelectedItems.Count <= 1)
            {
                if (sender is ListViewItem)
                {
                    listView_jcxx.SelectedIndex = -1;
                    ((ListViewItem)sender).IsSelected = true;
                }
                else
                {
                    listView_jcxx.SelectedIndex = -1;
                }
            }
        }

        private void listView_jcxx_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (listView_jcxx.SelectedItems.Count > 0)
            {

                
            }
            else
            {
                listView_jcxx.ContextMenu = null;

            }
        }

        private void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
           // menu_open_Click(null, null);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            liststr = new List<string>();
            liststr.Add("DeptName");
            liststr.Add("OfficeName");
            liststr.Add("RoomNumber");
            liststr.Add("UserName");
            liststr.Add("DeviceName");
            liststr.Add("OSType");
            liststr.Add("IPAddres");
            liststr.Add("MacAddress");

        }
    }
}
