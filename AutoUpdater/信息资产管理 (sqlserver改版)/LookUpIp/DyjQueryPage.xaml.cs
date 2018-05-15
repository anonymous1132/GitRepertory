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
    /// DyjQueryPage.xaml 的交互逻辑
    /// </summary>
    public partial class DyjQueryPage : Page
    {
        public DyjQueryPage()
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
                    sql = "select * from dyjfullquery";
                    break;
                case 1:
                    sql = "select * from dyjfullquery where department like'%" + comboBox_input.Text + "%'";
                    break;
                case 2:
                    sql = "select * from dyjfullquery where master like'%" + comboBox_input.Text + "%'";
                    break;
                case 3:
                    sql = "select * from dyjfullquery where pinpai like'%" + comboBox_input.Text + "%'";
                    break;
                case 4:
                    sql = "select * from dyjfullquery where xinghao like'%" + comboBox_input.Text + "%'";
                    break;
                case 5:
                    sql = "select * from dyjfullquery where IP like'%" + comboBox_input.Text + "%'";
                    break;
                case 6:
                    if (comboBox_input.Text == "其他") { sql = "select * from dyjfullquery where network <> '内网' and network <>'外网'"; }
                    else
                    {
                        sql = "select * from dyjfullquery where network like'%" + comboBox_input.Text + "%'";
                    }
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
                    case 2:
                        comboBox_input.IsEnabled = true;
                        comboBox_input.Items.Clear();
                        break;
                    case 3:
                        comboBox_input.IsEnabled = true;
                        comboBox_input.Items.Clear();
                        break;
                    case 4:
                        comboBox_input.IsEnabled = true;
                        comboBox_input.Items.Clear();
                        break;
                    case 5:
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
                    case 6:
                        comboBox_input.IsEnabled = true;
                        comboBox_input.Items.Clear();
                        comboBox_input.Items.Add("内网");
                        comboBox_input.Items.Add("外网");
                        comboBox_input.Items.Add("其他");
                        comboBox_input.IsDropDownOpen = true;
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
                openMenu.Header = "打开";
                openMenu.Click += menu_open_Click;
                if (listView_jcxx.SelectedItems.Count == 1) aMenu.Items.Add(openMenu);


                System.Windows.Controls.MenuItem outMenu = new System.Windows.Controls.MenuItem();
                outMenu.Header = "导出";
                outMenu.Click += menu_out_Click;
                aMenu.Items.Add(outMenu);

                System.Windows.Controls.MenuItem assetMenu = new System.Windows.Controls.MenuItem();
                assetMenu.Header = "生成资产卡";
                assetMenu.Click += menu_asset_Click;
                aMenu.Items.Add(assetMenu);


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
            saveFile.FileName = "打印机" + DateTime.Now.Date.ToShortDateString();
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
            dttemp.Columns[10].ColumnName = "是否A4";
            dttemp.Columns[11].ColumnName = "是否A3";
            dttemp.Columns[12].ColumnName = "是否扫描";
            dttemp.Columns[13].ColumnName = "是否复印";
            dttemp.Columns[14].ColumnName = "是否传真";
            dttemp.Columns[15].ColumnName = "MAC";
            dttemp.Columns[16].ColumnName = "IP";

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

        //打开
        private void menu_open_Click(object sender, RoutedEventArgs e)
        {
            DataRowView drv = listView_jcxx.SelectedItem as DataRowView;
            string sql = "";
            DYJWindow dyjw = new DYJWindow();
            sql = "select * from dyjfullquery where assets_NO ='" + drv["assets_NO"] + "'";
            accessOper aco = new accessOper();
            aco.getSomeData(sql, User.strConnection);
            DataRowView drv2 = aco.dt.DefaultView[0];
            dyjw.dyjv = DYJV(drv2);
            dyjw.DYJSureEvent += new DYJSureHandler(Reload);
            dyjw.ShowDialog();
        }
        private void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            menu_open_Click(null,null);
        }
        //生成资产卡
        private void menu_asset_Click_bak(object sender, RoutedEventArgs e)
        {
            if (listView_jcxx.SelectedItems.Count == 0)
            {
                System.Windows.MessageBox.Show("请先选中需要导出的记录");
                return;
            }
            string demopath = System.Windows.Forms.Application.StartupPath + @"\demo\demodyj.xlsx";
            if (!System.IO.File.Exists(demopath))
            {

                System.Windows.MessageBox.Show("请检查demo文件夹下的模板文件是否存在！");
                System.Windows.MessageBox.Show(demopath);
                return;
            }

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";

            if (dialog.ShowDialog() == DialogResult.OK)
            {

                string foldPath = dialog.SelectedPath;
                foreach (DataRowView drv in listView_jcxx.SelectedItems)
                {
                    if (drv != null && drv is DataRowView)
                    {
                        string department = drv.Row["department"].ToString();
                        string banzhu = drv.Row["banzhu"].ToString();
                        string master = drv.Row["master"].ToString();
                        string location = drv.Row["location"].ToString();
                        string assets_NO = drv.Row["assets_NO"].ToString();
                        string xinghao = drv.Row["xinghao"].ToString();
                        string pinpai = drv.Row["pinpai"].ToString();
                        string SN = drv.Row["SN"].ToString();
                        string zcgs = "";
                        if (assets_NO.IndexOf("JT") > -1) { zcgs = "集体企业"; }
                        else { zcgs = "海宁供电公司"; }
                        string made_date = drv.Row["made_date"].ToString();
                        string A4=drv.Row["A4"].ToString();
                        string A3 = drv.Row["A3"].ToString();
                        string scaner = drv.Row["scaner"].ToString();
                        string copyer = drv.Row["copyer"].ToString();
                        string IP=drv.Row["IP"].ToString();
                        string mac=drv.Row["MAC"].ToString();
                        string filepath = foldPath + "\\" + department + master+assets_NO + "_打印机.xlsx";
                        System.IO.File.Copy(demopath, filepath, true);
                        Exceloper exo = new Exceloper();
                        DataTable dt = exo.ExcelToDT(filepath, false, "demo");
                        dt.Rows[2][1] = department;
                        dt.Rows[2][4] = banzhu;
                        dt.Rows[3][1] = master;
                        dt.Rows[3][4] = location;
                        dt.Rows[5][1] = assets_NO;
                        dt.Rows[5][4] = zcgs;
                        dt.Rows[6][1] = pinpai;
                        dt.Rows[6][4] = xinghao;
                        dt.Rows[7][1] = SN;
                        dt.Rows[7][4] = made_date;
                        dt.Rows[8][1] = A4;
                        dt.Rows[8][4] = A3;
                        dt.Rows[9][1] = scaner;
                        dt.Rows[9][4] = copyer;
                        dt.Rows[10][1] = IP;
                        dt.Rows[10][4] = mac;
                        exo.ExExcel2(dt, filepath, master);
                    }

                }

                System.Windows.Forms.MessageBox.Show("已导出");
            }

        }

        private void menu_asset_Click(object sender, RoutedEventArgs e)
        {
            if (listView_jcxx.SelectedItems.Count == 0)
            {
                System.Windows.MessageBox.Show("请先选中需要导出的记录");
                return;
            }
            string demopath = System.Windows.Forms.Application.StartupPath + @"\demo\demodyj.xlsx";
            if (!System.IO.File.Exists(demopath))
            {

                System.Windows.MessageBox.Show("请检查demo文件夹下的模板文件是否存在！");
                System.Windows.MessageBox.Show(demopath);
                return;
            }

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";

            if (dialog.ShowDialog() == DialogResult.OK)
            {

                string foldPath = dialog.SelectedPath;
                foreach (DataRowView drv in listView_jcxx.SelectedItems)
                {
                    if (drv != null && drv is DataRowView)
                    {
                        string department = drv.Row["department"].ToString();
                        string banzhu = drv.Row["banzhu"].ToString();
                        string master = drv.Row["master"].ToString();
                        string location = drv.Row["location"].ToString();
                        string assets_NO = drv.Row["assets_NO"].ToString();
                        string xinghao = drv.Row["xinghao"].ToString();
                        string pinpai = drv.Row["pinpai"].ToString();
                        string SN = drv.Row["SN"].ToString();
                        string zcgs = "";
                        if (assets_NO.IndexOf("JT") > -1) { zcgs = "集体企业"; }
                        else { zcgs = "海宁供电公司"; }
                        string made_date = drv.Row["made_date"].ToString();
                        string A4 = drv.Row["A4"].ToString();
                        string A3 = drv.Row["A3"].ToString();
                        string scaner = drv.Row["scaner"].ToString();
                        string copyer = drv.Row["copyer"].ToString();
                        string IP = drv.Row["IP"].ToString();
                        string mac = drv.Row["MAC"].ToString();
                       // string lastupdate = "";
                        string switc = "";
                        string switchport = "";
                        string sql = "select * from IP where IPAddress ='"+drv["IP"].ToString()+"'";
                        accessOper aco = new accessOper();
                        aco.getSomeData(sql, User.strConnection);
                        if (aco.dt.DefaultView.Count > 0)
                        {
                           // lastupdate = aco.dt.DefaultView[0]["LastUsedTime"].ToString();
                            switc = aco.dt.DefaultView[0]["switch"].ToString();
                            switchport = aco.dt.DefaultView[0]["switchport"].ToString();
                        }

                        string filepath = foldPath + "\\" + department + master + assets_NO + "_打印机.xlsx";
                        System.IO.File.Copy(demopath, filepath, true);
                        Exceloper exo = new Exceloper();
                        DataTable dt = exo.ExcelToDT(filepath, false, "demo");
                        dt.Rows[2][1] = department;
                        dt.Rows[2][4] = banzhu;
                        dt.Rows[3][1] = master;
                        dt.Rows[3][4] = location;
                        dt.Rows[5][1] = assets_NO;
                        dt.Rows[5][4] = zcgs;
                        dt.Rows[6][1] = pinpai;
                        dt.Rows[6][4] = xinghao;
                        dt.Rows[7][1] = SN;
                        dt.Rows[7][4] = made_date;
                        dt.Rows[8][1] = A4;
                        dt.Rows[8][4] = A3;
                        dt.Rows[9][1] = scaner;
                        dt.Rows[9][4] = copyer;
                        dt.Rows[10][1] = IP;
                        dt.Rows[10][4] = mac;
                        dt.Rows[12][1] = switc;
                        dt.Rows[12][4] = switchport;

                        exo.ExExcel2(dt, filepath, master);
                    }

                }

                System.Windows.Forms.MessageBox.Show("已导出");
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
                        string sqltem = "delete from endingdevice where endingdevice.assets_NO ='" + id + "'";
                        aco.getSomeData(sqltem, User.strConnection);
                        sqltem = "delete from DYJ where assets_NO ='" + id + "'";
                        aco.getSomeData(sqltem, User.strConnection);
                    }

                    Reload();

                    System.Windows.MessageBox.Show("已删除选中记录");

                }
            }
        }

        private DYJViewModel DYJV(DataRowView drv)
        {
            DYJViewModel dyjv = new DYJViewModel();
            dyjv.assetsno = drv["assets_NO"].ToString();
            dyjv.banzhu = drv["banzhu"].ToString();
            dyjv.department = drv["department"].ToString();
            dyjv.ip = drv["IP"].ToString();
            string sql = "select * from IP where IPAddress ='" + drv["IP"].ToString() + "'";
            accessOper aco = new accessOper();
            aco.getSomeData(sql, User.strConnection);
            if (aco.dt.DefaultView.Count > 0)
            {
                dyjv.lastupdate = aco.dt.DefaultView[0]["LastUsedTime"].ToString();
                dyjv.switc = aco.dt.DefaultView[0]["switch"].ToString();
                dyjv.switchport = aco.dt.DefaultView[0]["switchport"].ToString();
                dyjv.mac_arp = aco.dt.DefaultView[0]["mac_arp"].ToString();
            }
            dyjv.location = drv["location"].ToString();
            dyjv.mac = drv["MAC"].ToString();
            dyjv.made_date = drv["made_date"].ToString();
            dyjv.master = drv["master"].ToString();
            dyjv.network = drv["network"].ToString();
            dyjv.pinpai = drv["pinpai"].ToString();
            dyjv.sn = drv["SN"].ToString();
            dyjv.xinghao = drv["xinghao"].ToString();
            return dyjv;
        }

        private void Reload()
        {
            if (sql != "")
            {
                accessOper aco = new accessOper();
                aco.getSomeData(sql, User.strConnection);
                Dispatcher.BeginInvoke(new Action(() => { this.listView_jcxx.ItemsSource = aco.dt.DefaultView; this.tb_count.Text = "共查询到" + listView_jcxx.Items.Count.ToString() + "条记录！"; }));

            }
        }
    }
}
