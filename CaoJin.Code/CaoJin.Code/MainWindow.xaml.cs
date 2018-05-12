using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CaoJin.Code
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ConvertModel cm ;
            switch (combobox.SelectedIndex)
            {
                case 0:
                    cm = new Sting2ASCIIConvertor();
                    break;
                case 1:
                    cm = new String2ASCIIHexConvertor();
                    break;
                case 2:
                    cm = new String2UnicodeConvertor();
                    break;
                case 3:
                    cm = new String2UnicodeHexConvertor();
                    break;
                default:
                    cm = new ConvertModel();
                    break;
            }

            cm.TextBoxContent = textbox.Text;
            try
            {
                cm.GetTextBlockContent();
            }
            catch (TextBoxInputException)
            {
                textblock.Text = "";
                return;
            }

            textblock.Text = cm.TextBlockContent;

        }
    }
}
