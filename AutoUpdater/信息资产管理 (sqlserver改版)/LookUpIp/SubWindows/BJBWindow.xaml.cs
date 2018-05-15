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
    /// BJBWindow.xaml 的交互逻辑
    /// </summary>
    public delegate void BJBSureHandler();
    public partial class BJBWindow : Window
    {
        public BJBWindow()
        {
            InitializeComponent();
        }
        public BJBViewModel bjbv;
        public ObservableCollection<EmployeeViewModel> obc_emp = new ObservableCollection<EmployeeViewModel>();
        public event BJBSureHandler BJBSureEvent;
        private bool isChanged = false;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            comb_department.Items.Clear();
            DataView dv = TsjQueryPage.GetDepartmentList().DefaultView;
            foreach (DataRowView drv in dv)
            {
                comb_department.Items.Add(drv[0].ToString());
            }
            Thread t = new Thread(GetRY);
            t.Start();
            this.DataContext = bjbv;
            bjbv.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(bjbv_Changed);
        }

        //值变化事件
        void bjbv_Changed(object sender, PropertyChangedEventArgs e)
        {
            isChanged = true;
            item_save.IsEnabled = true;
            tb_message.Text = "";
        }

        //获取用户列表
        static DataTable GetUserList(string department)
        {
            accessOper aco = new accessOper();
            string sql = "select username from employee where department ='" + department + "'";
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

        private void GetRY()
        {
           
            //获取人员信息
            if (bjbv != null && bjbv.master != "" && bjbv.master != null)
            {
                accessOper aco = new accessOper();
                string sql = "select * from employee where username like'%" + bjbv.master + "%'";
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
            string sql = "update BJB set cpu='" + bjbv.cpu + "', mem='" + bjbv.mem + "', disk='" + bjbv.disk + "', MAC='" + bjbv.mac + "', IP='" + bjbv.ip + "' where assets_NO='" + bjbv.assetsno + "'";
            aco.getSomeData(sql, User.strConnection);
            sql = "update endingdevice set xinghao='" + bjbv.xinghao + "', pinpai='" + bjbv.pinpai + "', SN='" + bjbv.sn + "', made_date='" + bjbv.made_date + "', department='" + bjbv.department + "', banzhu='" + bjbv.banzhu + "', master='" + bjbv.master + "', location='" + bjbv.location + "', network='" + bjbv.network + "' where assets_NO='" + bjbv.assetsno + "'";
            aco.getSomeData(sql, User.strConnection);
            isChanged = false;
            Dispatcher.BeginInvoke(new Action(() => { tb_message.Text = "保存成功！"; }));
            if (BJBSureEvent != null) BJBSureEvent();
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
