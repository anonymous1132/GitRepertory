using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Presentation;
using Caojin.Common;

namespace FirstFloor.ModernUI.App
{
    /// <summary>
    /// CaojinMainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CaojinMainWindow:ModernWindow
    {
        public CaojinMainWindow()
        {
            InitializeComponent();

            try
            {
                DataTable dt = XmlHelper.GetTable(@"App\config.xml", XmlHelper.XmlType.File, "AppearenceSetting");
                AppearanceManager.Current.AccentColor = (Color)ColorConverter.ConvertFromString(dt.Rows[0]["AccentColor"].ToString());
                AppearanceManager.Current.FontSize = dt.Rows[0]["FontSize"].ToString() == "Small" ? Presentation.FontSize.Small : Presentation.FontSize.Large;
                AppearanceManager.Current.ThemeSource = new Uri(dt.Rows[0]["ThemeSource"].ToString(), UriKind.Relative);
            }
            catch (Exception)
            {
            }
        }
    }
}
