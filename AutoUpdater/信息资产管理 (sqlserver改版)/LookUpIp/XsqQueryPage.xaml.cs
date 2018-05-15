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

namespace LookUpIp
{
    /// <summary>
    /// XsqQueryPage.xaml 的交互逻辑
    /// </summary>
    public partial class XsqQueryPage : Page
    {
        public XsqQueryPage()
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
                    sql = "select * from xsqfullquery";
                    break;
                case 1:
                    sql = "select * from xsqfullquery where department like'%" + comboBox_input.Text + "%'";
                    break;
                case 2:
                    sql = "select * from xsqfullquery where master like'%" + comboBox_input.Text + "%'";
                    break;
                case 3:
                    sql = "select * from xsqfullquery where pinpai like'%" + comboBox_input.Text + "%'";
                    break;
                case 4:
                    sql = "select * from xsqfullquery where xinghao like'%" + comboBox_input.Text + "%'";
                    break;
                case 5:
                    sql = "select * from tsjfullquery where SN like'%" + comboBox_input.Text + "%'";
                    break;

                case 6:
                    sql = "select * from tsjfullquery where IP like'%" + comboBox_input.Text + "%'";
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
                        DataView dv = GetDepartmentList().DefaultView;
                        foreach (DataRowView drv in dv)
                        {
                            comboBox_input.Items.Add(drv[0].ToString());
                        }
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
            aco.getSomeData(sql, User.strConnection);
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
            saveFile.FileName = "显示器" + DateTime.Now.Date.Year.ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Day.ToString();
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
            dttemp.Columns[10].ColumnName = "IP";;
            dttemp.Columns[11].ColumnName = "MAC";
            dttemp.Columns[12].ColumnName = "主机标识编号";
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
                        string sqltem = "delete from XSQ where assets_NO ='" + id + "'";
                        aco.getSomeData(sqltem, User.strConnection);

                    }

                    accessOper newaco = new accessOper();
                    newaco.getSomeData(sql, User.strConnection);
                    listView_jcxx.ItemsSource = newaco.dt.DefaultView;
                    this.tb_count.Text = "共查询到" + listView_jcxx.Items.Count.ToString() + "条记录！";
                    System.Windows.MessageBox.Show("已删除选中记录");

                }
            }
        }
    }
}
