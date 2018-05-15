﻿using System;
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
using System.Windows.Shapes;
using System.Data;
using CJComLibrary;
using LookUpIp.ViewModel;
using System.Threading;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MessageBox = System.Windows.Forms.MessageBox;

namespace LookUpIp.SubWindows
{
    /// <summary>
    /// TSJWindow.xaml 的交互逻辑
    /// </summary>
    public delegate void TSJSureHandler();
    public partial class TSJWindow : Window
    {
        public TSJWindow()
        {
            InitializeComponent();
        }
        public  TSJViewModel tsjv;
        public ObservableCollection<XSQViewModel> obc_xsq = new ObservableCollection<XSQViewModel>();
        public ObservableCollection<EmployeeViewModel> obc_emp = new ObservableCollection<EmployeeViewModel>();
        public event TSJSureHandler TSJSureEvent;
        private bool isChanged = false;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            comb_department.Items.Clear();
            DataView dv = TsjQueryPage.GetDepartmentList().DefaultView;
            foreach (DataRowView drv in dv)
            {
                comb_department.Items.Add(drv[0].ToString());
            }
            Thread t = new Thread(GetXSQ);
            t.Start();
            this.DataContext = tsjv;
            tsjv.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(tsjv_Changed);

        }

        //值变化事件
        void tsjv_Changed(object sender, PropertyChangedEventArgs e)
        {
            isChanged = true;
            item_save.IsEnabled = true;
            tb_message.Text = "";
        }

        //获取用户列表
        static DataTable GetUserList(string department)
        {
            accessOper aco = new accessOper();
            string sql = "select username from employee where department ='"+department+"'";
            aco.getSomeData(sql, User.strConnection);
            return aco.dt;
        }

        //用户下拉框更新
        private void comb_department_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comb_username.Items.Clear();
            DataView dv = GetUserList(comb_department.SelectedItem.ToString()).DefaultView;
            foreach (DataRowView drv in dv)
            {
                comb_username.Items.Add(drv[0].ToString());
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                switch (comb_pinpai.SelectedIndex)
                {
                    case 0:
                        comb_xinghao.Items.Clear();
                        comb_xinghao.Items.Add("EliteDesk 800 G2 SFF");
                        comb_xinghao.Items.Add("ProDesk 600 G2 SFF");
                        comb_xinghao.Items.Add("EliteDesk 800 G3 Tower PC");
                        comb_xinghao.Items.Add("Compaq dc 7800");
                        comb_xinghao.Items.Add("Compaq dc 7900");
                        break;
                    case 1:
                        comb_xinghao.Items.Clear();
                        comb_xinghao.Items.Add("ThinkCentre M8500t");
                        comb_xinghao.Items.Add("ThinkCentre M8400t");
                        comb_xinghao.Items.Add("ThinkCentre M6100t");
                        break;

                    case 2:
                        comb_xinghao.Items.Clear();
                        comb_xinghao.Items.Add("OptiPlex 745");
                        comb_xinghao.Items.Add("OptiPlex 755");
                        comb_xinghao.Items.Add("OptiPlex GX620");
                        break;

                }
            }
            catch (Exception)
            { }
        }

        private void GetXSQ()
        {
            if (tsjv!=null && tsjv.assetsno != "" && tsjv.assetsno != null)
            {
                accessOper aco = new accessOper();
                string sql = "select * from XSQ where tsj_assets_NO ='" + tsjv.assetsno + "'";
                aco.getSomeData(sql, User.strConnection);
                List<XSQViewModel> list = ModelConvertHelper<XSQViewModel>.ConvertIListToList(ModelConvertHelper<XSQViewModel>.ConvertToModel(aco.dt));
                if (list != null)
                {
                    obc_xsq = new ObservableCollection<XSQViewModel>(list);
                }
           
                Dispatcher.BeginInvoke(new Action(() => { this.dg.ItemsSource = obc_xsq; }));
            }

            //获取人员信息
            if (tsjv != null && tsjv.master != "" && tsjv.master != null)
            {
                accessOper aco = new accessOper();
                string sql = "select * from employee where username like'%" + tsjv.master + "%'";
                aco.getSomeData(sql, User.strConnection);
                obc_emp = ModelConvertHelper<EmployeeViewModel>.ConvertToObc(aco.dt);
                Dispatcher.BeginInvoke(new Action(() => { this.dg_phone.ItemsSource = obc_emp; }));
            }
        }



        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.comb_department.Focus();
            item_save.IsEnabled = false;
            Thread uploadThread = new Thread(UploadDate);
            uploadThread.Start();
          
        }

        private void UploadDate()
        {
            accessOper aco = new accessOper();
            string sql = "update TSJ set cpu='"+ tsjv.cpu + "', mem='"+tsjv.mem+"', disk='"+tsjv.disk+"', MAC='"+tsjv.mac+"', IP='"+tsjv.ip+"', OS='"+tsjv.os+"' where assets_NO='"+tsjv.assetsno+"'";
            aco.getSomeData(sql,User.strConnection);
            sql = "update endingdevice set xinghao='"+tsjv.xinghao+"', pinpai='"+tsjv.pinpai+"', SN='"+tsjv.sn+"', made_date='"+tsjv.made_date+"', department='"+tsjv.department+"', banzhu='"+tsjv.banzhu+"', master='"+tsjv.master+"', location='"+tsjv.location+"', network='"+tsjv.network+"' where assets_NO='"+tsjv.assetsno+"'";
            aco.getSomeData(sql, User.strConnection);
            isChanged = false;
            Dispatcher.BeginInvoke(new Action(() => { tb_message.Text = "保存成功！"; }));
            if (TSJSureEvent != null) TSJSureEvent();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (isChanged == false) return;
            switch (MessageBox.Show("当前修改并没有被保存，确认退出吗？\n 是=保存并退出，否=不保存并退出，取消=取消退出", "提示", System.Windows.Forms.MessageBoxButtons.YesNoCancel, System.Windows.Forms.MessageBoxIcon.Warning))
            {
                case System.Windows.Forms.DialogResult.Yes:
                    UploadDate();
                    break;
                case System.Windows.Forms.DialogResult.No:
                   
                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }
    
    }
}
