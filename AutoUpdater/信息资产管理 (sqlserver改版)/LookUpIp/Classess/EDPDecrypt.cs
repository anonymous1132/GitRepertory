using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CJComLibrary;
using System.Data;

namespace LookUpIp.Classess
{
     public class EDPDecrypt
    {
         private EDPDecrypt() { }
         private static byte[] GetDecrypt(byte[] byteResources)
         {
             int bslen = byteResources.Length;
             byte[] result = new byte[bslen];
             result = BitAssist.ExchangeTargetBit(0, 6, byteResources);
             result = BitAssist.ExchangeTargetBit(2, 3, result);
             result = BitAssist.ExchangeTargetBit(4, 5, result);
             return result;
         }

         public static string GetDecryptString(string miwen)
         {
             byte[] arry = UTF8Encoding.Default.GetBytes(miwen);
             arry = GetDecrypt(arry);
             return ASCIIEncoding.Default.GetString(arry);
         }

         public static DataTable GetDecryptDT(DataTable dtmiwen ,List<string>liststr)
         {
             if (dtmiwen == null) return null;
             if (dtmiwen.DefaultView.Count <= 0) return null;
             for (int i = 0; i < dtmiwen.Columns.Count; i++)
             {
                 if (liststr.Contains(dtmiwen.Columns[i].ColumnName))
                 {
                     for (int j = 0; j < dtmiwen.DefaultView.Count; j++)
                     {
                         try
                         {
                             dtmiwen.DefaultView[j][i] = GetDecryptString(dtmiwen.DefaultView[j][i].ToString());
                         }
                         catch (Exception)
                         { return null; }
                     }
                 
                 
                 }
             }

                 return dtmiwen;
         }
    }


}
