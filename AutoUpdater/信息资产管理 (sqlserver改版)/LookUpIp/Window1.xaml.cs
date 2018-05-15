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
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void AssetManageButton_Click(object sender, RoutedEventArgs e)
        {
            this.frame.Navigate(new Uri("AssetPage.xaml", UriKind.Relative));
        }
        

        private void IPQueryButton_Click(object sender, RoutedEventArgs e)
        {
            this.frame.Navigate(new Uri("IPPage.xaml", UriKind.Relative));
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }


        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void OperateButton_Click(object sender, RoutedEventArgs e)
        {
            this.frame.Navigate(new Uri("OperatePage.xaml", UriKind.Relative));
        }

        private void GKQueryButton_Click(object sender, RoutedEventArgs e)
        {
            this.frame.Navigate(new Uri("EDPQueryPage.xaml", UriKind.Relative));
        }


        
    }
}
