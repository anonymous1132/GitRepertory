using FirstFloor.ModernUI.Windows.Controls;
using System.Data;
using FirstFloor.ModernUI.Presentation;
using Caojin.Common;
using System.Windows.Media;
using System;

namespace FirstFloor.ModernUI.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        public MainWindow()
        {
            //InitializeComponent();
            try
            {
               DataTable dt= XmlHelper.GetTable(@"App\config.xml", XmlHelper.XmlType.File, "AppearenceSetting");
               AppearanceManager.Current.AccentColor = (Color)ColorConverter.ConvertFromString(dt.Rows[0]["AccentColor"].ToString());
               AppearanceManager.Current.FontSize = dt.Rows[0]["FontSize"].ToString() == "Small" ? Presentation.FontSize.Small : Presentation.FontSize.Large;
               AppearanceManager.Current.ThemeSource = new Uri(dt.Rows[0]["ThemeSource"].ToString(),UriKind.Relative);
            }
            catch (Exception)
            {
            }
            
        }
    }
}
