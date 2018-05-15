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

namespace LookUpIp
{
    /// <summary>
    /// AssetPage.xaml 的交互逻辑
    /// </summary>
    public partial class AssetPage : Page
    {
        public AssetPage()
        {
            InitializeComponent();
        }

        private void TsjQueryButton_Click(object sender, RoutedEventArgs e)
        {
            this.frame.Navigate(new Uri("TsjQueryPage.xaml", UriKind.Relative));
        }

        private void DyjQueryButton_Click(object sender, RoutedEventArgs e)
        {
            this.frame.Navigate(new Uri("DyjQueryPage.xaml", UriKind.Relative));
        }

        private void YzdQueryButton_Click(object sender, RoutedEventArgs e)
        {
            this.frame.Navigate(new Uri("YzdQueryPage.xaml", UriKind.Relative));
        }

        private void BjbQueryButton_Click(object sender, RoutedEventArgs e)
        {
            this.frame.Navigate(new Uri("BjbQueryPage.xaml", UriKind.Relative));
        }

        private void JhjQueryButton_Click(object sender, RoutedEventArgs e)
        {
            this.frame.Navigate(new Uri("JhjQueryPage.xaml", UriKind.Relative));
        }

        private void FwqQueryButton_Click(object sender, RoutedEventArgs e)
        {
            this.frame.Navigate(new Uri("FwqQueryPage.xaml", UriKind.Relative));
        }

        private void OtherQueryButton_Click(object sender, RoutedEventArgs e)
        {
            this.frame.Navigate(new Uri("OtherQueryPage.xaml", UriKind.Relative));
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            this.frame.Navigate(new Uri("UploadPage.xaml", UriKind.Relative));
        }

        private void XsqQueryButton_Click(object sender, RoutedEventArgs e)
        {
            this.frame.Navigate(new Uri("XsqQueryPage.xaml", UriKind.Relative));
        }
    }
}
