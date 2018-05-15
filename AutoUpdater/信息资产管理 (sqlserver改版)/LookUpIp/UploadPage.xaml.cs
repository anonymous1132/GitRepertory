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
using System.Windows.Forms;
using System.Data;
using System.Threading;
using CJComLibrary;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace LookUpIp
{
    /// <summary>
    /// UploadPage.xaml 的交互逻辑
    /// </summary>
    public partial class UploadPage : Page
    {
        public UploadPage()
        {
            InitializeComponent();
        }

        string tb_content = "操作日志";
        string filepath = "";
        string filetype = "";
        List<string> tsj;
        List<string> dyj;
        List<string> yzd;
        List<string> net;
        List<string> bjb;
        List<string> fwq;
        List<string> jhj;
        List<string> endingdevice;
        List<string> xsq;
        Dictionary<string, List<string>> diclist;
        Dictionary<string, string> diclisttable;
        Dictionary<string, string> diclistcolumn;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
             tsj = new List<string> { "cpu", "内存", "硬盘容量", "MAC", "IP","操作系统" };
            dyj = new List<string> {   "是否A4", "是否A3", "是否扫描", "是否复印", "是否传真", "MAC", "IP" };
            yzd = new List<string> {  "账号", "用户名称", "终端IP", "终端MAC", "虚拟机IP", "OC/公共盘用户" };
            net = new List<string> {   "MAC", "IP", "用途" };
            bjb = new List<string> { "cpu", "内存", "硬盘容量", "MAC", "IP"};
             fwq = new List<string> {"服务器应用","cpu","内存","硬盘容量","MAC","IP","操作系统" };
             jhj = new List<string> {"IP", "运行状态", "以太网口速度", "以太网口数量", "光口模块类型", "光口数量", "上柜日期","汇聚层" };
             endingdevice = new List<string> { "部门", "使用人", "班组", "安装地点", "品牌", "型号", "出厂日期", "网络类型", "标识编号","序列号" };
             xsq = new List<string> { "标识编号", "品牌", "型号", "出厂日期", "序列号","主机标识编号" }; ;
            diclist = new Dictionary<string, List<string>>();
            diclist.Add("台式机$", tsj);
            diclist.Add("笔记本$", bjb);
            diclist.Add("云终端$", yzd);
            diclist.Add("打印机$", dyj);
            diclist.Add("内网接入设备$", net);
            diclist.Add("服务器$", fwq);
            diclist.Add("交换机$", jhj);
            diclist.Add("显示器$", xsq);
            //diclist.Add("扫描仪", "品牌");
            //diclist.Add("投影仪", "品牌");
            //diclist.Add("传真机", "传真号");

            //建立表名字典
            diclisttable = new Dictionary<string, string>();
            diclisttable.Add("台式机$", "TSJ");
            diclisttable.Add("笔记本$", "BJB");
            diclisttable.Add("云终端$", "YZD");
            diclisttable.Add("打印机$", "DYJ");
            diclisttable.Add("交换机$", "JHJ");
            diclisttable.Add("服务器$", "FWQ");
            diclisttable.Add("内网接入设备$", "NET");
            diclisttable.Add("显示器$", "XSQ");
            //建立字段字典
            diclistcolumn = new Dictionary<string, string>();
            diclistcolumn.Add("部门", "department");
            diclistcolumn.Add("使用人", "master");
            diclistcolumn.Add("班组", "banzhu");
            diclistcolumn.Add("安装地点", "location");
            diclistcolumn.Add("品牌", "pinpai");
            diclistcolumn.Add("型号", "xinghao");
            diclistcolumn.Add("出厂日期", "made_date");
            diclistcolumn.Add("序列号", "SN");
            diclistcolumn.Add("网络类型", "network");
            diclistcolumn.Add("cpu", "cpu");
            diclistcolumn.Add("内存", "mem");
            diclistcolumn.Add("硬盘容量", "disk");
            diclistcolumn.Add("MAC", "MAC");
            diclistcolumn.Add("IP", "IP");
            diclistcolumn.Add("标识编号", "assets_NO");
            diclistcolumn.Add("是否A4", "A4");
            diclistcolumn.Add("是否A3", "A3");
            diclistcolumn.Add("是否扫描", "scaner");
            diclistcolumn.Add("是否复印", "copyer");
            diclistcolumn.Add("是否传真", "sender");
            diclistcolumn.Add("账号", "login_account");
            diclistcolumn.Add("用户名称", "login_username");
            diclistcolumn.Add("终端IP", "IP");
            diclistcolumn.Add("终端MAC", "MAC");
            diclistcolumn.Add("虚拟机IP", "vm_IP");
            diclistcolumn.Add("OC/公用盘用户", "jxep_username");
            diclistcolumn.Add("品牌型号", "xinghao");
            diclistcolumn.Add("用途", "application");
            diclistcolumn.Add("服务器应用", "application");
            diclistcolumn.Add("业务部门", "department");
            diclistcolumn.Add("操作系统", "OS");
            diclistcolumn.Add("运行状态", "station");
            diclistcolumn.Add("以太网口速度", "rj45_speed");
            diclistcolumn.Add("以太网口数量", "rj45_quan");
            diclistcolumn.Add("光口模块类型", "op_model");
            diclistcolumn.Add("光口数量", "op_quan");
            diclistcolumn.Add("上柜日期", "last_located_date");
            diclistcolumn.Add("汇聚层", "huiju");
            diclistcolumn.Add("主机标识编号", "tsj_assets_NO");
        }
        private void textbox_files_MouseEnter(object sender, MouseEventArgs e)
        {
            textbox_files.Text = filepath;
        }

        private void textbox_files_MouseLeave(object sender, MouseEventArgs e)
        {
            if (filepath.Length > 43)
            {
                textbox_files.Text = "..." + filepath.Substring(filepath.Length - 40, 40);
            }
        }

        private void button_startupload_Click(object sender, RoutedEventArgs e)
        {
            if (filepath == "") return;
            button_startupload.IsEnabled = false;
            if (!System.IO.File.Exists(filepath)) { tb_resault.Text = tb_resault.Text + "\n当前选择文件不存在！"; button_startupload.IsEnabled = true; return; }

            Exceloper exo = new Exceloper();
            DataSet ds = exo.ExcelToDS(filepath, true);
            if (!checkfile(ref  ds)) { button_startupload.IsEnabled = true; return; }
            uploadfile(ds);
            button_startupload.IsEnabled = true;
        }

        private void button_selectfile_Click(object sender, RoutedEventArgs e)
        {
            selectfile();
            if (filepath.Length > 43)
            {
                textbox_files.Text = "..." + filepath.Substring(filepath.Length - 40, 40);
            }
            else { textbox_files.Text = filepath; }
            if (filepath.Length > 0)
            {
                tb_content = tb_content + "\n当前已选择文件：" + filepath;
                tb_resault.Text = tb_content;
            }
        }

        private void selectfile()
        {
            filepath = "";
            filetype = ""; ;
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Excel(*.xlsx)|*.xlsx|Excel(*.xls)|*.xls|All Files(*.*)|*.*";
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFile.Multiselect = false;
            if (openFile.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            filepath = openFile.FileName;
            filetype = System.IO.Path.GetExtension(filepath);

        }

        private void uploadfile(DataSet ds)
        {
            if (ds.Tables.Count <= 0) return;
            accessOper aco = new accessOper();
            string sql = "";

            for (int i = 0; i < ds.Tables.Count; i++)
            {

                try
                {
                    tb_resault.Text = tb_resault.Text + "\n正在导入中，请稍后！";
                    string tablename=diclisttable[ds.Tables[i].TableName];
                    List<string> listcol = diclist[ds.Tables[i].TableName];
                    int rownum = 0;
                    if (tablename != "XSQ")
                    {
                         sql = "select assets_NO from endingdevice";
                        aco.getSomeData(sql, User.strConnection);
                        foreach (DataRowView drv in ds.Tables[i].DefaultView)
                        {
                            rownum++;
                            string columns = "devicekind";
                            string values = "'" + ds.Tables[i].TableName.Replace("$", "") + "'";
                            string columns2 = "assets_NO";
                            string values2 = "'" + drv["标识编号"].ToString() + "'";
                            if (drv["标识编号"].ToString() == "" || drv["标识编号"].ToString() == null)
                            {

                                tb_resault.Text = tb_resault.Text + "\n表[" + ds.Tables[i].TableName + "] 行" + rownum.ToString() + " 标识编号为空，自动跳过该行";
                                continue;
                            }
                            aco.dt.DefaultView.RowFilter = "assets_NO = '" + drv["标识编号"] + "'";
                            if (aco.dt.DefaultView.Count > 0) { tb_resault.Text = tb_resault.Text + "\n表[" + ds.Tables[i].TableName + "] 行" + rownum.ToString() + " 标识编号有重复值，自动跳过该行"; continue; }
                            foreach (string value in endingdevice)
                            {
                                string strcolname = diclistcolumn[value];
                                columns = columns + ", " + strcolname; values = values + ", '" + drv[value].ToString() + "'";
                            }

                            accessOper acoupload = new accessOper();
                            string sqlupload = "insert into endingdevice (" + columns + ") values (" + values + ")";

                            
                            foreach (string value in listcol)
                            {
                                string strcolname = diclistcolumn[value];
                                columns2 = columns2 + ", " + strcolname; values2 = values2 + ", '" + drv[value].ToString() + "'";
                            }
                            string sqlupload2 = "insert into " + tablename + "(" + columns2 + ") values (" + values2 + ")";

                            acoupload.getSomeData(sqlupload, User.strConnection);
                            acoupload.getSomeData(sqlupload2, User.strConnection);
                        }
                    }
                    else 
                    {
                        sql = "select assets_NO from XSQ";
                        aco.getSomeData(sql, User.strConnection);
                       
                        foreach (DataRowView drv in ds.Tables[i].DefaultView)
                        {
                            rownum++;
                            string columns2 = "";
                            string values2 = "";

                            if (drv["标识编号"].ToString() == "" || drv["标识编号"].ToString() == null)
                            {

                                tb_resault.Text = tb_resault.Text + "\n表[" + ds.Tables[i].TableName + "] 行" + rownum.ToString() + " 标识编号为空，自动跳过该行";
                                continue;
                            }
                            aco.dt.DefaultView.RowFilter = "assets_NO = '" + drv["标识编号"] + "'";
                            if (aco.dt.DefaultView.Count > 0) { tb_resault.Text = tb_resault.Text + "\n表[" + ds.Tables[i].TableName + "] 行" + rownum.ToString() + " 标识编号有重复值，自动跳过该行"; continue; }
                            foreach (string value in listcol)
                            {
                                string strcolname = diclistcolumn[value];
                               
                                columns2 = columns2 + ", " + strcolname;
                               
                                values2 = values2 + ", '" + drv[value].ToString() + "'";
                            }
                            columns2 = columns2.Substring(1,columns2.Length-1);
                            values2 = values2.Substring(1,values2.Length-1);
                            accessOper acoupload = new accessOper();
                            string sqlupload2 = "insert into " + tablename + "(" + columns2 + ") values (" + values2 + ")";
                            acoupload.getSomeData(sqlupload2, User.strConnection);
                        }
 
                    }
                   
                }
                catch (Exception e)
                {
                    tb_resault.Text = tb_resault.Text + "\n表[" + ds.Tables[i].TableName + "]发生不明错误，自动跳过该表";
                    tb_resault.Text = tb_resault.Text + e.ToString();
                }
            }
            tb_resault.Text = tb_resault.Text + "\n上传结束！";
        }

        private bool checkfile(ref DataSet ds)
        {
            bool isok = true;
           
            for (int i = ds.Tables.Count-1; i >=0; i--)
            {
               List<string> listcol = new List<string>();
                try
                {
                    
                    
                    if(ds.Tables[i].TableName!="显示器$"){
                    listcol =endingdevice.Union(diclist[ds.Tables[i].TableName]).ToList<string>();}
                    else
                    {
                        listcol = diclist[ds.Tables[i].TableName];
                    }
                   
                    foreach (string value in listcol)
                    {
                        bool hasthekey = ds.Tables[i].Columns.Contains(value);
                        if (!hasthekey) 
                        { 
                            tb_resault.Text = tb_resault.Text + "\n表[" + ds.Tables[i].TableName + "]未包含" + value + "列，自动跳过该表";
                            ds.Tables.RemoveAt(i);
                            break;
                        }

                    }
                }
                catch (Exception) 
                {
                    tb_resault.Text = tb_resault.Text + "\n表[" + ds.Tables[i].TableName + "]表名不在规定范围内,自动跳过该表！";
                    ds.Tables.RemoveAt(i);
                }
            }
            if (ds.Tables.Count == 0)
            {
                isok = false;
                tb_resault.Text = tb_resault.Text + "\n检查完成，有效表数量为0！";
            }
            else
            { tb_resault.Text = tb_resault.Text + "\n检查完成，有效表数量为"+ ds.Tables.Count.ToString()+",下面将进行上传！"; }
            return isok;
        
        }

      
    }

    
}
