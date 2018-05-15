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
    /// BjbQueryPage.xaml 的交互逻辑
    /// </summary>
    public partial class BjbQueryPage : Page
    {
        public BjbQueryPage()
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
                    sql = "select * from bjbfullquery";
                    break;
                case 1:
                    sql = "select * from bjbfullquery where department like'%" + comboBox_input.Text + "%'";
                    break;
                case 2:
                    sql = "select * from bjbfullquery where master like'%" + comboBox_input.Text + "%'";
                    break;
                case 3:
                    sql = "select * from bjbfullquery where pinpai like'%" + comboBox_input.Text + "%'";
                    break;
                case 4:
                    sql = "select * from bjbfullquery where xinghao like'%" + comboBox_input.Text + "%'";
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
            saveFile.FileName = "笔记本" + DateTime.Now.Date.Year.ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Day.ToString();
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
            dttemp.Columns[10].ColumnName = "CPU";
            dttemp.Columns[11].ColumnName = "内存";
            dttemp.Columns[12].ColumnName = "硬盘容量";
            dttemp.Columns[13].ColumnName = "MAC";
            dttemp.Columns[14].ColumnName = "IP";

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
        private void menu_asset_Click(object sender, RoutedEventArgs e)
        {
            if (listView_jcxx.SelectedItems.Count == 0)
            {
                System.Windows.MessageBox.Show("请先选中需要导出的记录");
                return;
            }
            string demopath = System.Windows.Forms.Application.StartupPath + @"\demo\demobjb.xlsx";
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
                        string mem = drv.Row["mem"].ToString();
                        string disk = drv.Row["disk"].ToString();
                        string cpu = drv.Row["cpu"].ToString();
                        string network = drv.Row["network"].ToString();
                        string IP = drv.Row["IP"].ToString();
                        string mac = drv.Row["MAC"].ToString();

                        string filepath = foldPath + "\\" + department + master +assets_NO+ "_笔记本.xlsx";
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
                        dt.Rows[8][1] = mem;
                        dt.Rows[8][4] = disk;
                        dt.Rows[9][1] = cpu;
                        dt.Rows[9][4] = network;
                        dt.Rows[10][1] = IP;
                        dt.Rows[10][4] = mac;
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
                        string sqltem = "delete from endingdevice where assets_NO ='" + id + "'";
                        aco.getSomeData(sqltem, User.strConnection);
                        sqltem = "delete from BJB where assets_NO ='" + id + "'";
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
                BJBWindow bjbw = new BJBWindow();
                bjbw.bjbv = BJBV(drv);
                bjbw.BJBSureEvent += new BJBSureHandler(Reload);
                bjbw.ShowDialog();
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
        }
        private   BJBViewModel BJBV(DataRowView drv)
        {
            BJBViewModel bjbv = new BJBViewModel();
            bjbv.assetsno = drv["assets_NO"].ToString();
            bjbv.banzhu = drv["banzhu"].ToString();
            bjbv.cpu = drv["cpu"].ToString();
            bjbv.department = drv["department"].ToString();
            bjbv.disk = drv["disk"].ToString();
            bjbv.ip = drv["IP"].ToString();
            bjbv.location = drv["location"].ToString();
            bjbv.mac = drv["MAC"].ToString();
            bjbv.made_date = drv["made_date"].ToString();
            bjbv.master = drv["master"].ToString();
            bjbv.mem = drv["mem"].ToString();
            bjbv.network = drv["network"].ToString();
            bjbv.pinpai = drv["pinpai"].ToString();
            bjbv.sn = drv["SN"].ToString();
            bjbv.xinghao = drv["xinghao"].ToString();
            return  bjbv;
        }
    }
}
