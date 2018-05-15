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

namespace CJComLibrary
{
    public sealed class SubListView : ListView
    {

        protected override void PrepareContainerForItemOverride(System.Windows.DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            if (View is GridView)
            {
                int index = ItemContainerGenerator.IndexFromContainer(element); // The ItemContainerGenerator has method to get index for a given Item  
                ListViewItem lvi = element as ListViewItem;
                if (index % 2 == 0)
                { 
                    lvi.Background = Brushes.LightBlue;
                    lvi.Foreground = Brushes.Black;
                }

                else
                {
                    lvi.Background = Brushes.LightGray;
                    lvi.Foreground = Brushes.Black;
                }
            }
        }
    }
}
