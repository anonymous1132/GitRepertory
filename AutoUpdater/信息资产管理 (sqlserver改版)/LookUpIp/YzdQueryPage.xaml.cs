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
using CJComLibrary;
using System.Data;
using ListViewItem = System.Windows.Controls.ListViewItem;
using Button = System.Windows.Controls.Button;
using LookUpIp.Extention;
using System.Windows.Forms;
using LookUpIp.ViewModel;
using System.Collections.ObjectModel;
using LookUpIp.SubWindows;

namespace LookUpIp
{
    /// <summary>
    /// YzdQueryPage.xaml 的交互逻辑
    /// </summary>
    public partial class YzdQueryPage : Page
    {
        public YzdQueryPage()
        {
            InitializeComponent();
        }
        string sql = "";
        private void button_quickfind_Click(object sender, RoutedEventArgs e)
        {
            this.button_quickfind.IsEnabled = false;
            switch (comboBox_queryitem.SelectedIndex)
            {
                case 0:
                    sql = "select * from yzdfullquery";
                    break;
                case 1:
                    sql = "select * from yzdfullquery where department like'%" + comboBox_input.Text + "%'";
                    break;
                case 2:
                    sql = "select * from yzdfullquery where master like'%" + comboBox_input.Text + "%'";
                    break;
                case 3:
                    sql = "select * from yzdfullquery where IP like'%" + comboBox_input.Text + "%'";
                    break;
                case 4:
                    sql = "select * from yzdfullquery where login_account like'%" + comboBox_input.Text + "%'";
                    break;
            }

            if (sql != "" && sql != null)
            {
                accessOper aco = new accessOper();
                aco.getSomeData(sql, User.strConnection);
                this.listView_jcxx.ItemsSource = aco.dt.DefaultView;
            }
            this.tb_count.Text = "共查询到" + listView_jcxx.Items.Count.ToString() + "条记录！";
            this.button_quickfind.IsEnabled = true;
        }

        private void comboBox_queryitem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                switch (comboBox_queryitem.SelectedIndex)
                {
                    case 0:
                        comboBox_input.Text = "";
                        comboBox_input.IsEnabled = false;
                        break;
                    case 1:
                        comboBox_input.IsEnabled = true;
                        comboBox_input.Items.Clear();
                        DataView dv = TsjQueryPage.GetDepartmentList().DefaultView;
                        foreach (DataRowView drv in dv)
                        {
                            comboBox_input.Items.Add(drv[0].ToString());
                        }
                        comboBox_input.IsDropDownOpen = true;
                        break;

                    case 3:
                        comboBox_input.IsEnabled = true;
                        comboBox_input.Items.Clear();
                        comboBox_input.Items.Add("10.147.184.");
                        comboBox_input.Items.Add("10.147.185.");
                        comboBox_input.Items.Add("10.147.186.");
                        comboBox_input.Items.Add("10.147.187.");
                        comboBox_input.Items.Add("10.147.188.");
                        comboBox_input.Items.Add("10.147.189.");
                        comboBox_input.Items.Add("10.147.190.");
                        comboBox_input.Items.Add("10.147.191.");
                        comboBox_input.IsDropDownOpen = true;
                        break;
                    default:
                        comboBox_input.IsEnabled = true;
                        comboBox_input.Items.Clear();
                        break;
                }
            }
            catch (Exception)
            { }
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

                System.Windows.Controls.ContextMenu aMenu = new System.Windows.Controls.ContextMenu();
                System.Windows.Controls.MenuItem openMenu = new System.Windows.Controls.MenuItem();
                //openMenu.Header = "打开";
                //openMenu.Click += menu_open_Click;
                //if (listView_jcxx.SelectedItems.Count ==1) aMenu.Items.Add(openMenu);


                System.Windows.Controls.MenuItem outMenu = new System.Windows.Controls.MenuItem();
                outMenu.Header = "导出";
                outMenu.Click += menu_out_Click;
                aMenu.Items.Add(outMenu);


                System.Windows.Controls.MenuItem deleteMenu = new System.Windows.Controls.MenuItem();
                deleteMenu.Header = "删除";
                deleteMenu.Click += menu_shanchu_Click;
                aMenu.Items.Add(deleteMenu);


                listView_jcxx.ContextMenu = aMenu;
            }
            else
            {
                listView_jcxx.ContextMenu = null;

            }
        }

        //导出
        private void menu_out_Click(object sender, RoutedEventArgs e)
        {
            if (listView_jcxx.SelectedItems.Count == 0)
            {
                System.Windows.MessageBox.Show("请先选中需要导出的记录");
                return;
            }
            SaveFileDialog saveFile = new SaveFileDialog() { Filter = "CSV Files (*.csv)|*.csv|Excel Files (*.xlsx)|*.xlsx|All   files (*.*)|*.*" };
            saveFile.Title = "导出文件路径";
            saveFile.FilterIndex = 2;
            saveFile.FileName = "云终端" + DateTime.Now.Date.Year.ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Day.ToString();
            if (saveFile.ShowDialog() == DialogResult.Cancel) return;
            //    StringBuilder strBuilder = new StringBuilder();
            string strFilePath = saveFile.FileName;
            DataTable dttemp = new DataTable();
            dttemp = ((DataView)listView_jcxx.ItemsSource).Table.Clone();
            foreach (DataRowView drv in listView_jcxx.SelectedItems)
            {
                if (drv != null && drv is DataRowView)
                {
                    DataRow dr = ((DataView)listView_jcxx.ItemsSource).Table.NewRow();
                    dr = drv.Row;
                    dttemp.ImportRow(dr);
                }
            }

            dttemp.Columns.Remove("devicekind");
            dttemp.Columns[0].ColumnName = "标识编号";
            dttemp.Columns[1].ColumnName = "部门";
            dttemp.Columns[2].ColumnName = "使用人";
            dttemp.Columns[3].ColumnName = "班组";
            dttemp.Columns[4].ColumnName = "安装地点";
            dttemp.Columns[5].ColumnName = "品牌";
            dttemp.Columns[6].ColumnName = "型号";
            dttemp.Columns[7].ColumnName = "出厂日期";
            dttemp.Columns[8].ColumnName = "序列号";
            dttemp.Columns[9].ColumnName = "网络类型";
            dttemp.Columns[10].ColumnName = "账号";
            dttemp.Columns[11].ColumnName = "用户名称";
            dttemp.Columns[12].ColumnName = "终端IP";
            dttemp.Columns[13].ColumnName = "终端MAC";
            dttemp.Columns[14].ColumnName = "虚拟机IP";
            dttemp.Columns[15].ColumnName = "OC/公共盘用户";

            try
            {
                Exceloper exo = new Exceloper();
                exo.ExExcel(dttemp, strFilePath);
                System.Windows.MessageBox.Show("已保存");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("出错:" + ex.ToString());
            }

        }

        //删除
        private void menu_shanchu_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("确认删除当前选中记录吗？删除后将不可恢复！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {

                if (listView_jcxx.SelectedItems.Count > 0)
                {
                    foreach (DataRowView drv in listView_jcxx.SelectedItems)
                    {
                        string id = drv["assets_NO"].ToString();
                        accessOper aco = new accessOper();
                        string sqltem = "delete from endingdevice where assets_NO ='" + id + "'";
                        aco.getSomeData(sqltem, User.strConnection);
                        sqltem = "delete from YZD where assets_NO ='" + id + "'";
                        aco.getSomeData(sqltem, User.strConnection);
                    }

                    Reload();

                    System.Windows.MessageBox.Show("已删除选中记录");

                }
            }
        }

        //双击列表事件
        private void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView drv = listView_jcxx.SelectedItem as DataRowView;
            if (drv != null && drv is DataRowView)
            {
                YZDWindow yzdw = new YZDWindow();
                yzdw.yzdv = YZDV(drv);
                yzdw.YZDSureEvent += new YZDSureHandler(Reload);
                yzdw.ShowDialog();
            }

        }

        private void Reload()
        {
            if (sql != "")
            {
                accessOper aco = new accessOper();
                aco.getSomeData(sql, User.strConnection);
                Dispatcher.BeginInvoke(new Action(() => { this.listView_jcxx.ItemsSource = aco.dt.DefaultView; this.tb_count.Text = "共查询到" + listView_jcxx.Items.Count.ToString() + "条记录！"; }));

            }
            //  System.Windows.Forms.MessageBox.Show(sql);
        }

        private YZDViewModel YZDV(DataRowView drv)
        {
            YZDViewModel yzdv = new YZDViewModel();
            yzdv.assetsno = drv["assets_NO"].ToString();
            yzdv.banzhu = drv["banzhu"].ToString();
            yzdv.vm_IP = drv["vm_IP"].ToString();
            yzdv.department = drv["department"].ToString();
            yzdv.jxep_username = drv["jxep_username"].ToString();
            yzdv.ip = drv["IP"].ToString();
            string sql = "select * from IP where IPAddress ='" + drv["IP"].ToString() + "'";
            accessOper aco = new accessOper();
            aco.getSomeData(sql, User.strConnection);
            if (aco.dt.DefaultView.Count > 0)
            {
                yzdv.lastupdate = aco.dt.DefaultView[0]["LastUsedTime"].ToString();
                yzdv.switc = aco.dt.DefaultView[0]["switch"].ToString();
                yzdv.switchport = aco.dt.DefaultView[0]["switchport"].ToString();
                yzdv.mac_arp = aco.dt.DefaultView[0]["mac_arp"].ToString();
            }
            yzdv.location = drv["location"].ToString();
            yzdv.mac = drv["MAC"].ToString();
            yzdv.made_date = drv["made_date"].ToString();
            yzdv.master = drv["master"].ToString();
            yzdv.login_account = drv["login_account"].ToString();
            yzdv.network = drv["network"].ToString();
            yzdv.login_username = drv["login_username"].ToString();
            yzdv.pinpai = drv["pinpai"].ToString();
            yzdv.sn = drv["SN"].ToString();
            yzdv.xinghao = drv["xinghao"].ToString();
            return yzdv;
        }
    }
}
