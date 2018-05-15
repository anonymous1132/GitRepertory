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
    /// TsjQueryPage.xaml 的交互逻辑
    /// </summary>
    public partial class TsjQueryPage : Page
    {
        public TsjQueryPage()
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
                    sql = "select * from tsjfullquery";
                    break;
                case 1:
                    sql = "select * from tsjfullquery where department like'%"+comboBox_input.Text+"%'";
                    break;
                case 2:
                    sql = "select * from tsjfullquery where master like'%" + comboBox_input.Text + "%'";
                    break;
                case 3:
                    sql = "select * from tsjfullquery where pinpai like'%" + comboBox_input.Text + "%'";
                    break;
                case 4:
                    sql = "select * from tsjfullquery where xinghao like'%" + comboBox_input.Text + "%'";
                    break;
                case 5:
                    sql = "select * from tsjfullquery where OS like'%" + comboBox_input.Text + "%'";
                    break;
                case 6:
                    sql = "select * from tsjfullquery where IP like'%" + comboBox_input.Text + "%'";
                    break;
                case 7:
                    if (comboBox_input.Text == "其他") { sql = "select * from tsjfullquery where network <> '内网' and network <>'外网'"; }
                    else
                    {
                        sql = "select * from tsjfullquery where network like'%" + comboBox_input.Text + "%'";
                    }
                    break;
                case 8:
                    sql = "select * from tsjfullquery where location like'%" + comboBox_input.Text + "%'";
                    break;
                case 9:
                     sql = "select * from tsjfullquery where SN like'%" + comboBox_input.Text + "%'";
                    break;


            }


            if (sql != "" && sql != null)
            {
                accessOper aco = new accessOper();
                aco.getSomeData(sql, User.strConnection);
                this.listView_jcxx.ItemsSource = aco.dt.DefaultView;
                this.tb_count.Text = listView_jcxx.Items.Count.ToString();
            }
            this.tb_count.Text ="共查询到"+ listView_jcxx.Items.Count.ToString()+"条记录！";
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
                        comboBox_input.IsEnabled=false;
                        break;
                    case 1:
                        comboBox_input.IsEnabled = true;
                        comboBox_input.Items.Clear();
                        DataView dv = GetDepartmentList().DefaultView;
                        foreach (DataRowView drv in dv)
                        {
                            comboBox_input.Items.Add(drv[0].ToString());
                        }
                        comboBox_input.IsDropDownOpen = true;
                        break;
                   
                    case 5:
                         comboBox_input.IsEnabled = true;
                        comboBox_input.Items.Clear();
                        comboBox_input.Items.Add("Win7");
                        comboBox_input.Items.Add("XP");
                        comboBox_input.IsDropDownOpen = true;
                        break;
                    case 6:
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
                    case 7:
                         comboBox_input.IsEnabled = true;
                        comboBox_input.Items.Clear();
                        comboBox_input.Items.Add("内网");
                        comboBox_input.Items.Add("外网");
                        comboBox_input.Items.Add("其他");
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


        //获取部门列表
        public static DataTable GetDepartmentList()
        {
            accessOper aco = new accessOper();
            string sql = "select distinct department from employee";
            aco.getSomeData(sql,User.strConnection);
            return aco.dt;
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
            saveFile.FileName = "计算机" + DateTime.Now.Date.Year.ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Day.ToString();
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
            dttemp.Columns[10].ColumnName = "cpu";
            dttemp.Columns[11].ColumnName = "内存";
            dttemp.Columns[12].ColumnName = "硬盘容量";
            dttemp.Columns[13].ColumnName = "MAC";
            dttemp.Columns[14].ColumnName = "IP";
            dttemp.Columns[15].ColumnName = "操作系统";
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

        //生成资产卡
        private void menu_asset_Click_bak(object sender, RoutedEventArgs e)
        {
            if (listView_jcxx.SelectedItems.Count == 0)
            {
                System.Windows.MessageBox.Show("请先选中需要导出的记录");
                return;
            }
            string demopath = System.Windows.Forms.Application.StartupPath + @"\demo\demotsj.xlsx";
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
                        string master=drv.Row["master"].ToString();
                        string location=drv.Row["location"].ToString();
                        string assets_NO=drv.Row["assets_NO"].ToString();
                        string xinghao=drv.Row["xinghao"].ToString();
                        string pinpai=drv.Row["pinpai"].ToString();
                        string SN=drv.Row["SN"].ToString();
                        string zcgs = "";
                        if (assets_NO.IndexOf("JT") > -1) { zcgs = "集体企业"; }
                        else { zcgs = "海宁供电公司"; }
                        string made_date = drv.Row["made_date"].ToString();
                        string mem=drv.Row["mem"].ToString();
                        string disk = drv.Row["disk"].ToString();
                        string cpu=drv.Row["cpu"].ToString();
                        string network=drv.Row["network"].ToString();
                        string IP=drv.Row["IP"].ToString();
                        string mac = drv.Row["MAC"].ToString();
                        string mor_assets_NO = "";
                        string mor_zcgs = "";
                        string mor_pinpai = "";
                        string mor_xinghao = "";
                        string mor_SN = "";
                        string mor_made_date = "";
                        accessOper acoo = new accessOper();
                        string sql2 = "select * from XSQ where tsj_assets_NO='"+assets_NO +"'";
                        acoo.getSomeData(sql2,User.strConnection);
                        DataView dv2=acoo.dt.DefaultView;
                        if (acoo.dt.DefaultView.Count > 0)
                        {
                            mor_assets_NO =dv2[0]["assets_NO"].ToString();
                            if (mor_assets_NO.IndexOf("JT") > -1) { mor_zcgs = "集体企业"; }
                            else { mor_zcgs = "海宁供电公司"; }
                            mor_pinpai = dv2[0]["pinpai"].ToString();
                            mor_xinghao = dv2[0]["xinghao"].ToString();
                            mor_SN = dv2[0]["SN"].ToString();
                            mor_made_date = dv2[0]["made_date"].ToString();
                        }


                        string filepath = foldPath + "\\" + department + master +assets_NO+ "_台式机.xlsx";
                        System.IO.File.Copy(demopath, filepath, true);
                        Exceloper exo = new Exceloper();
                        DataTable dt = exo.ExcelToDT(filepath,false,"demo");
                        dt.Rows[2][1]=department;
                        dt.Rows[2][4]=banzhu;
                        dt.Rows[3][1] = master;
                        dt.Rows[3][4] = location;
                        dt.Rows[5][1] = assets_NO;
                        dt.Rows[5][4] = zcgs;
                        dt.Rows[6][1] = pinpai;
                        dt.Rows[6][4] = xinghao;
                        dt.Rows[7][1] = SN;
                        dt.Rows[7][4] = made_date;
                        dt.Rows[8][1] = mem;
                        dt.Rows[8][4] = disk;
                        dt.Rows[9][1] = cpu;
                        dt.Rows[9][4] = network;
                        dt.Rows[10][1] = IP;
                        dt.Rows[10][4] = mac;
                        dt.Rows[12][1] = mor_assets_NO;
                        dt.Rows[12][4] = mor_zcgs;
                        dt.Rows[13][1] = mor_pinpai;
                        dt.Rows[13][4] = mor_xinghao;
                        dt.Rows[14][1] = mor_SN;
                        dt.Rows[14][4] = mor_made_date;


                        exo.ExExcel2(dt,filepath,master);
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
            string demopath = System.Windows.Forms.Application.StartupPath + @"\demo\demotsj.xlsx";
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
                        dt.Rows[10][4] = tsjv.mac;
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
                        string sqltem = "delete from endingdevice where assets_NO ='" + id + "'";
                        aco.getSomeData(sqltem,User.strConnection);
                        sqltem = "delete from TSJ where assets_NO ='" + id + "'";
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
                TSJWindow tsjw = new TSJWindow();
                tsjw.tsjv = TSJV(drv);
                tsjw.TSJSureEvent += new TSJSureHandler(Reload);
                tsjw.ShowDialog();
            }

        }
        private void Reload()
        {
            if (sql != "")
            {
                accessOper aco = new accessOper();
                aco.getSomeData(sql, User.strConnection);
                Dispatcher.BeginInvoke(new Action(() => { 
                    this.listView_jcxx.ItemsSource = aco.dt.DefaultView;
                    this.tb_count.Text = "共查询到" + listView_jcxx.Items.Count.ToString() + "条记录！";
                }));
               
            }
         //  System.Windows.Forms.MessageBox.Show(sql);
        }
        private TSJViewModel TSJV(DataRowView drv)
        {
            TSJViewModel tsjv = new TSJViewModel();
            tsjv.assetsno=drv["assets_NO"].ToString();
            tsjv.banzhu=drv["banzhu"].ToString();
            tsjv.cpu=drv["cpu"].ToString();
            tsjv.department=drv["department"].ToString();
            tsjv.disk=drv["disk"].ToString();
            tsjv.ip=drv["IP"].ToString();
            string sql = "select * from IP where IPAddress ='"+drv["IP"].ToString()+"'";
            accessOper aco = new accessOper();
            aco.getSomeData(sql,User.strConnection);
            if (aco.dt.DefaultView.Count > 0)
            {
                tsjv.lastupdate = aco.dt.DefaultView[0]["LastUsedTime"].ToString();
                tsjv.switc = aco.dt.DefaultView[0]["switch"].ToString();
                tsjv.switchport = aco.dt.DefaultView[0]["switchport"].ToString();
                tsjv.mac_arp = aco.dt.DefaultView[0]["mac_arp"].ToString();
            }
            tsjv.location=drv["location"].ToString();
            tsjv.mac=drv["MAC"].ToString();
            tsjv.made_date=drv["made_date"].ToString();
            tsjv.master=drv["master"].ToString();
            tsjv.mem=drv["mem"].ToString();
            tsjv.network=drv["network"].ToString();
            tsjv.os=drv["OS"].ToString();
            tsjv.pinpai=drv["pinpai"].ToString();
            tsjv.sn=drv["SN"].ToString();
            tsjv.xinghao = drv["xinghao"].ToString();
            return tsjv;
        }

    }
}
