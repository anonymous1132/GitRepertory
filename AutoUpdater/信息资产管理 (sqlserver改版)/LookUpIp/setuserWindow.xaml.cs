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

namespace LookUpIp
{
    /// <summary>
    /// setuserWindow.xaml 的交互逻辑
    /// </summary>
     public delegate void CloseHandler(string user);
    public partial class setuserWindow : Window
    {
        public setuserWindow()
        {
            InitializeComponent();
        }
        public string username = "";
        public event CloseHandler CloseEvent;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            username = this.textbox.Text;
            if (CloseEvent != null)
            {
                CloseEvent(this.username);
            }
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.textbox.Text = username;

        }
    }
}
