using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caojin.Common;
using System.Data;
using FirstFloor.ModernUI.Presentation;

namespace FirstFloor.ModernUI.App.Runner
{
   public class XmlWriter
    {
        public XmlWriter()
        { }

        public static void WriteAppearenceSetting(string xmlFilePath)
        {
            DataTable dt = new DataTable("AppearenceSetting");
            dt.Columns.Add("ThemeSource",typeof(string));
            dt.Columns.Add("FontSize",typeof(string));
            dt.Columns.Add("AccentColor",typeof(string));
            DataRow dr = dt.NewRow();
            dr["ThemeSource"] = AppearanceManager.Current.ThemeSource;
            dr["FontSize"] = AppearanceManager.Current.FontSize;
            dr["AccentColor"]= AppearanceManager.Current.AccentColor;
            dt.Rows.Add(dr);
            XmlHelper.SaveTableToFile(dt,xmlFilePath);
        }
    }
}
