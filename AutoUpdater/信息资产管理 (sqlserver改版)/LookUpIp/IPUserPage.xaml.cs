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
using LookUpIp.SubWindows;
using LookUpIp.Classess;

namespace LookUpIp
{
    /// <summary>
    /// IPUserPage.xaml 的交互逻辑
    /// </summary>
    public partial class IPUserPage : Page
    {
        public IPUserPage()
        {
            InitializeComponent();
        }
        string sql = "";
        //List<IPInfoViewModel> list = new List<IPInfoViewModel>();
        //ObservableCollection<IPInfoViewModel> obc = new ObservableCollection<IPInfoViewModel>();

        private void button_quickfind_Click(object sender, RoutedEventArgs e)
        {
            this.button_quickfind.IsEnabled = false;
            switch (comboBox_queryitem.SelectedIndex)
            {
                case 0:
                    sql = "select * from IP_INSIDE ";
                    break;
                case 1:
                    sql = "select * from IP_INSIDE where IPAddress like'%" + comboBox_input.Text + "%'";
                    break;
                case 2:
                    sql = "select * from WaitForBack" ;
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
                case 8:
                    sql = "select * from YufenpeiOrFireWall";
                    break;
                case 9:
                    sql = "select * from ChangedIP where mac_arp not like '00-26-%'";
                    break;
            }

            if (sql != "" && sql != null)
            {
                try
                {
                    sql = sql + " order by ID";
                    accessOper aco = new accessOper();
                    aco.getSomeData(sql, User.strConnection);
                    this.listView_jcxx.ItemsSource = aco.dt.DefaultView;
                }
                catch (Exception) { this.listView_jcxx.ItemsSource = null; }
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
                        comboBox_input.IsEditable = true;
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

                    case 3:
                        comboBox_input.IsEnabled = true;
                        comboBox_input.Items.Clear();
                        comboBox_input.IsEditable = false;
                         comboBox_input.Items.Add("TRUE");
                        comboBox_input.Items.Add("FALSE");
                        comboBox_input.SelectedIndex = 0;
                        comboBox_input.IsDropDownOpen = true;
                        break;
                    case 4:
                        comboBox_input.IsEnabled = true;
                        comboBox_input.Items.Clear();
                        comboBox_input.IsEditable = true;
                        DataView dv = TsjQueryPage.GetDepartmentList().DefaultView;
                        foreach (DataRowView drv in dv)
                        {
                            comboBox_input.Items.Add(drv[0].ToString());
                        }
                        comboBox_input.IsDropDownOpen = true;
                        break;
                    case 5:
                        comboBox_input.IsEnabled = true;
                        comboBox_input.Items.Clear();
                        comboBox_input.IsEditable = true;
                        break;
                    case 6:
                        comboBox_input.IsEnabled = true;
                        comboBox_input.Items.Clear();
                        comboBox_input.IsEditable = true;
                        comboBox_input.Items.Add("台式机");
                        comboBox_input.Items.Add("打印机");
                        comboBox_input.Items.Add("服务器");
                        comboBox_input.Items.Add("云终端");
                        comboBox_input.IsDropDownOpen = true;
                        break;
                    case 7:
                        comboBox_input.IsEnabled = true;
                        comboBox_input.Items.Clear();
                        comboBox_input.IsEditable = true;
                         comboBox_input.Items.Add("172.30.221.");
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
                System.Windows.Controls.MenuItem clearMenu = new System.Windows.Controls.MenuItem();
                clearMenu.Header = "IP回收";
                clearMenu.Click += menu_clear_Click;

                if (listView_jcxx.SelectedItems.Count == 1)
                {
                    aMenu.Items.Add(openMenu);
                    aMenu.Items.Add(clearMenu);
                }

                System.Windows.Controls.MenuItem outMenu = new System.Windows.Controls.MenuItem();
                outMenu.Header = "导出";
                outMenu.Click += menu_out_Click;
                aMenu.Items.Add(outMenu);

                System.Windows.Controls.MenuItem assetMenu = new System.Windows.Controls.MenuItem();
                assetMenu.Header = "导出资产卡片";
                assetMenu.Click += menu_asset_Click;
                aMenu.Items.Add(assetMenu);

                listView_jcxx.ContextMenu = aMenu;
            }
            else
            {
                listView_jcxx.ContextMenu = null;

            }
        }

        //生产资产卡片
        private void menu_asset_Click(object sender, RoutedEventArgs e)
        {
            if (listView_jcxx.SelectedItems.Count == 0)
            {
                System.Windows.MessageBox.Show("请先选中需要导出的记录");
                return;
            }
            string demopath_tsj = System.Windows.Forms.Application.StartupPath + @"\demo\demotsj.xlsx";
            if (!System.IO.File.Exists(demopath_tsj))
            {

                System.Windows.MessageBox.Show("请检查demo文件夹下的台式机模板文件是否存在！");
                System.Windows.MessageBox.Show(demopath_tsj);
                return;
            }

            string demopath_dyj = System.Windows.Forms.Application.StartupPath + @"\demo\demodyj.xlsx";
            if (!System.IO.File.Exists(demopath_dyj))
            {

                System.Windows.MessageBox.Show("请检查demo文件夹下的打印机模板文件是否存在！");
                System.Windows.MessageBox.Show(demopath_dyj);
                return;
            }
            string demopath_net = System.Windows.Forms.Application.StartupPath + @"\demo\demonet.xlsx";
            if (!System.IO.File.Exists(demopath_net))
            {

                System.Windows.MessageBox.Show("请检查demo文件夹下的内网接入设备模板文件是否存在！");
                System.Windows.MessageBox.Show(demopath_net);
                return;
            }

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                string sql = "";
                accessOper aco = new accessOper();
                foreach (DataRowView drv in listView_jcxx.SelectedItems)
                {

                    if(drv["devicekind"].ToString() == "台式机")
                    {
                         sql = "select * from tsjfullquery where assets_NO ='" + drv["assets_NO"] + "'";
                         aco.getSomeData(sql,User.strConnection);
                        ZCTSJ(aco.dt.DefaultView[0],demopath_tsj,foldPath); 
                    }
                    else if (drv["devicekind"].ToString() == "打印机")
                    {
                        sql = "select * from dyjfullquery where assets_NO ='" + drv["assets_NO"] + "'";
                        aco.getSomeData(sql, User.strConnection);
                        ZCDYJ(aco.dt.DefaultView[0], demopath_dyj, foldPath); 
                    }
                    else if (drv["devicekind"].ToString() == "内网接入设备")
                    {
                        sql = "select * from netfullquery where assets_NO ='" + drv["assets_NO"] + "'";
                        aco.getSomeData(sql, User.strConnection);
                        ZCNET(aco.dt.DefaultView[0], demopath_net, foldPath);
                    }
                }

                System.Windows.Forms.MessageBox.Show("已导出");
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
            saveFile.FileName = "IP申请一览" + DateTime.Now.Date.Year.ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Day.ToString();
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

            dttemp.Columns.Remove("ID");
            dttemp.Columns.Remove("IP");
            dttemp.Columns[0].ColumnName = "IP";
            dttemp.Columns[1].ColumnName = "是否使用";
            dttemp.Columns[2].ColumnName = "发现时间";
            dttemp.Columns[3].ColumnName = "所属交换机";
            dttemp.Columns[4].ColumnName = "交换机端口";
            dttemp.Columns[5].ColumnName = "绑定MAC";
            dttemp.Columns[6].ColumnName = "型号";
            dttemp.Columns[7].ColumnName = "品牌";
            dttemp.Columns[8].ColumnName = "序列号";
            dttemp.Columns[9].ColumnName = "出厂日期";
            dttemp.Columns[10].ColumnName = "登记部门";
            dttemp.Columns[11].ColumnName = "登记用户";
            dttemp.Columns[12].ColumnName = "登记班组";
            dttemp.Columns[13].ColumnName = "登记安装位置";
            dttemp.Columns[14].ColumnName = "登记设备类型";
            dttemp.Columns[15].ColumnName = "登记网络类型";
            dttemp.Columns[16].ColumnName = "登记设备标识编号";
            dttemp.Columns[17].ColumnName = "登记MAC";
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
           // System.Windows.MessageBox.Show(drv["devicekind"].ToString())
            string sql = "";
            if (drv["devicekind"].ToString()=="台式机")
            {
                TSJWindow tsjw = new TSJWindow();
                sql = "select * from tsjfullquery where assets_NO ='"+drv["assets_NO"]+"'";
                accessOper aco = new accessOper();
                aco.getSomeData(sql,User.strConnection);
               DataRowView drv2 = aco.dt.DefaultView[0];
                tsjw.tsjv = TSJV(drv2);
                tsjw.TSJSureEvent += new TSJSureHandler(Reload);
                tsjw.ShowDialog();
            }
            else if (drv["devicekind"].ToString() == "打印机")
            {
                DYJWindow dyjw = new DYJWindow();
                sql = "select * from dyjfullquery where assets_NO ='" + drv["assets_NO"] + "'";
                accessOper aco = new accessOper();
                aco.getSomeData(sql, User.strConnection);
                DataRowView drv2 = aco.dt.DefaultView[0];
                dyjw.dyjv = DYJV(drv2);
                dyjw.DYJSureEvent += new DYJSureHandler(Reload);
                dyjw.ShowDialog();
            }
            else if (drv["devicekind"].ToString() == "内网接入设备")
            {
                NETWindow netw = new NETWindow();
                sql = "select * from netfullquery where assets_NO ='" + drv["assets_NO"] + "'";
                accessOper aco = new accessOper();
                aco.getSomeData(sql, User.strConnection);
                DataRowView drv2 = aco.dt.DefaultView[0];
                netw.netv = NETV(drv2);
                netw.NETSureEvent += new NETSureHandler(Reload);
                netw.ShowDialog();
            }
            else if (drv["devicekind"].ToString() == "云终端")
            {
                YZDWindow yzdw = new YZDWindow();
                sql = "select * from yzdfullquery where assets_NO ='" + drv["assets_NO"] + "'";
                accessOper aco = new accessOper();
                aco.getSomeData(sql, User.strConnection);
                DataRowView drv2 = aco.dt.DefaultView[0];
                yzdw.yzdv = YZDV(drv2);
                yzdw.YZDSureEvent += new YZDSureHandler(Reload);
                yzdw.ShowDialog();
            }

        }
        private void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            menu_open_Click(null,null);
        }
        //回收
        private void menu_clear_Click(object sender, RoutedEventArgs e)
        {
            if (listView_jcxx.SelectedItems.Count == 0)
            {
                System.Windows.MessageBox.Show("请先选中对象");
                return;
            }
            DataRowView drv = listView_jcxx.SelectedItem as DataRowView;
            // System.Windows.MessageBox.Show(drv["devicekind"].ToString())
            string sql = "";

            HNIP_InNet_Business hnip = new HNIP_InNet_Business(System.Net.IPAddress.Parse(drv["IPAddress"].ToString()));
            if (hnip.iscommon)
            {
                sql = "update IP set isused = 0 ,LastUsedTime = null, switch='', switchport='' where IPAddress ='" + drv["IPAddress"].ToString() + "'";
            }
            else
            {
                sql = "update IP_Second set isused = 0 ,LastUsedTime = null, switch='', switchport='' where IPAddress ='" + drv["IPAddress"].ToString() + "'";
            }

            accessOper aco = new accessOper();
            aco.getSomeData(sql, User.strConnection);
            try
            {
                AutoArp.backip(System.Net.IPAddress.Parse(drv["IPAddress"].ToString()));
            }
            catch (Exception ex) { System.Windows.MessageBox.Show(ex.Message); }
            Reload();
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
        private TSJViewModel TSJV(DataRowView drv)
        {
            TSJViewModel tsjv = new TSJViewModel();
            tsjv.assetsno = drv["assets_NO"].ToString();
            tsjv.banzhu = drv["banzhu"].ToString();
            tsjv.cpu = drv["cpu"].ToString();
            tsjv.department = drv["department"].ToString();
            tsjv.disk = drv["disk"].ToString();
            tsjv.ip = drv["IP"].ToString();
            string sql = "select * from IP_INSIDE where IPAddress ='" + drv["IP"].ToString() + "'";
            accessOper aco = new accessOper();
            aco.getSomeData(sql, User.strConnection);
            if (aco.dt.DefaultView.Count > 0)
            {
                tsjv.lastupdate = aco.dt.DefaultView[0]["LastUsedTime"].ToString();
                tsjv.switc = aco.dt.DefaultView[0]["switch"].ToString();
                tsjv.switchport = aco.dt.DefaultView[0]["switchport"].ToString();
                tsjv.mac_arp = aco.dt.DefaultView[0]["mac_arp"].ToString();
            }
            tsjv.location = drv["location"].ToString();
            tsjv.mac = drv["MAC"].ToString();
            tsjv.made_date = drv["made_date"].ToString();
            tsjv.master = drv["master"].ToString();
            tsjv.mem = drv["mem"].ToString();
            tsjv.network = drv["network"].ToString();
            tsjv.os = drv["OS"].ToString();
            tsjv.pinpai = drv["pinpai"].ToString();
            tsjv.sn = drv["SN"].ToString();
            tsjv.xinghao = drv["xinghao"].ToString();
            return tsjv;
        }
        private DYJViewModel DYJV(DataRowView drv)
        {
            DYJViewModel dyjv = new DYJViewModel();
            dyjv.assetsno = drv["assets_NO"].ToString();
            dyjv.banzhu = drv["banzhu"].ToString();
            dyjv.department = drv["department"].ToString();
            dyjv.ip = drv["IP"].ToString();
            string sql = "select * from IP_INSIDE where IPAddress ='" + drv["IP"].ToString() + "'";
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
        private NETViewModel NETV(DataRowView drv)
        {
            NETViewModel netv = new NETViewModel();
            netv.assetsno = drv["assets_NO"].ToString();
            netv.banzhu = drv["banzhu"].ToString();
            netv.department = drv["department"].ToString();
            netv.ip = drv["IP"].ToString();
            string sql = "select * from IP_INSIDE where IPAddress ='" + drv["IP"].ToString() + "'";
            accessOper aco = new accessOper();
            aco.getSomeData(sql, User.strConnection);
            if (aco.dt.DefaultView.Count > 0)
            {
                netv.lastupdate = aco.dt.DefaultView[0]["LastUsedTime"].ToString();
                netv.switc = aco.dt.DefaultView[0]["switch"].ToString();
                netv.switchport = aco.dt.DefaultView[0]["switchport"].ToString();
                netv.mac_arp = aco.dt.DefaultView[0]["mac_arp"].ToString();
            }
            netv.location = drv["location"].ToString();
            netv.mac = drv["MAC"].ToString();
            netv.made_date = drv["made_date"].ToString();
            netv.master = drv["master"].ToString();
            netv.network = drv["network"].ToString();
            netv.pinpai = drv["pinpai"].ToString();
            netv.sn = drv["SN"].ToString();
            netv.xinghao = drv["xinghao"].ToString();
            netv.application = drv["application"].ToString();
            return netv;
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
        private void ZCTSJ(DataRowView drv,string demopath,string foldPath)
        {
            TSJViewModel tsjv = TSJV(drv);
            string mor_assets_NO = "";
            string mor_zcgs = "";
            string mor_pinpai = "";
            string mor_xinghao = "";
            string mor_SN = "";
            string mor_made_date = "";
            accessOper acoo = new accessOper();
            string sql2 = "select * from XSQ where tsj_assets_NO='" + tsjv.assetsno + "'";
            acoo.getSomeData(sql2, User.strConnection);
            DataView dv2 = acoo.dt.DefaultView;
            if (acoo.dt.DefaultView.Count > 0)
            {
                mor_assets_NO = dv2[0]["assets_NO"].ToString();
                if (mor_assets_NO.IndexOf("JT") > -1) { mor_zcgs = "集体企业"; }
                else { mor_zcgs = "海宁供电公司"; }
                mor_pinpai = dv2[0]["pinpai"].ToString();
                mor_xinghao = dv2[0]["xinghao"].ToString();
                mor_SN = dv2[0]["SN"].ToString();
                mor_made_date = dv2[0]["made_date"].ToString();
            }
            string filepath = foldPath + "\\" + tsjv.department + tsjv.master + tsjv.assetsno + "_台式机.xlsx";
            System.IO.File.Copy(demopath, filepath, true);
            Exceloper exo = new Exceloper();
            DataTable dt = exo.ExcelToDT(filepath, false, "demo");
            dt.Rows[2][1] = tsjv.department;
            dt.Rows[2][4] = tsjv.banzhu;
            dt.Rows[3][1] = tsjv.master;
            dt.Rows[3][4] = tsjv.location;
            dt.Rows[5][1] = tsjv.assetsno;
            dt.Rows[5][4] = tsjv.belongs;
            dt.Rows[6][1] = tsjv.pinpai;
            dt.Rows[6][4] = tsjv.xinghao;
            dt.Rows[7][1] = tsjv.sn;
            dt.Rows[7][4] = tsjv.made_date;
            dt.Rows[8][1] = tsjv.mem;
            dt.Rows[8][4] = tsjv.disk;
            dt.Rows[9][1] = tsjv.cpu;
            dt.Rows[9][4] = tsjv.network;
            dt.Rows[10][1] = tsjv.ip;
            dt.Rows[10][4] = tsjv.mac_arp;
            dt.Rows[12][1] = mor_assets_NO;
            dt.Rows[12][4] = mor_zcgs;
            dt.Rows[13][1] = mor_pinpai;
            dt.Rows[13][4] = mor_xinghao;
            dt.Rows[14][1] = mor_SN;
            dt.Rows[14][4] = mor_made_date;
            dt.Rows[16][1] = tsjv.switc;
            dt.Rows[16][4] = tsjv.switchport;
            exo.ExExcel2(dt, filepath, tsjv.master);
        }
        private void ZCDYJ(DataRowView drv, string demopath, string foldPath)
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
            string mac = "";
            // string lastupdate = "";
            string switc = "";
            string switchport = "";
            string sql = "select * from IP where IPAddress ='" + drv["IP"].ToString() + "'";
            accessOper aco = new accessOper();
            aco.getSomeData(sql, User.strConnection);
            if (aco.dt.DefaultView.Count > 0)
            {
                // lastupdate = aco.dt.DefaultView[0]["LastUsedTime"].ToString();
                switc = aco.dt.DefaultView[0]["switch"].ToString();
                switchport = aco.dt.DefaultView[0]["switchport"].ToString();
                mac = aco.dt.DefaultView[0]["mac_arp"].ToString();
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
        private void ZCNET(DataRowView drv, string demopath, string foldPath)
        {
            NETViewModel netv = NETV(drv);
            string filepath = foldPath + "\\" + netv.department + netv.master + netv.assetsno + "_内网接入设备.xlsx";
            System.IO.File.Copy(demopath, filepath, true);
            Exceloper exo = new Exceloper();
            DataTable dt = exo.ExcelToDT(filepath, false, "demo");
            dt.Rows[2][1] = netv.department;
            dt.Rows[2][4] = netv.banzhu;
            dt.Rows[3][1] = netv.master;
            dt.Rows[3][4] = netv.location;
            dt.Rows[5][1] = netv.assetsno;
            dt.Rows[5][4] = netv.belongs;
            dt.Rows[6][1] = netv.pinpai;
            dt.Rows[6][4] = netv.xinghao;
            dt.Rows[7][1] = netv.sn;
            dt.Rows[7][4] = netv.made_date;
            dt.Rows[8][1] = netv.ip;
            dt.Rows[8][4] = netv.mac_arp;
            dt.Rows[9][1] = netv.application;
            dt.Rows[11][1] = netv.switc;
            dt.Rows[11][4] = netv.switchport;
            exo.ExExcel2(dt, filepath, netv.master);
        }
        
    }

}
