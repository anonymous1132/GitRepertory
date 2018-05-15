using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace SnmpTest
{
    class StringOper
    {
        public string MACFormat(string mac)
        {
            string str = mac.Replace(".","");
            str = str.Replace("-","");
            str = str.Replace(" ","");
            str = str.Replace(":", "");
            if (str.Length != 12) return null;
           return str.ToUpper();
        }
        public string MACFormat_line(string mac)
        {
          string str=  MACFormat(mac);
          if (str == null) return null;
          for (int i = 10; i >= 2; i = i - 2)
          {
              str = str.Insert(i,"-");
          }
          return str;
        }

        public string MACFormat_switch(string mac)
        {
            string str = mac.Replace(".", "");
            str = str.Replace("-", "");
            str = str.Replace(" ", "");
            str = str.Replace(":", "");
            if (str.Length != 12) return null;
            str = str.ToLower();
            str = str.Insert(8, ".");
            str = str.Insert(4, ".");
            return str;
        }



        public string MACFormat_ten(string mac)
        {
          string str =  MACFormat(mac);
          if (str == null) return null;
            int[] a=new int[6];
            for (int i = 10; i >= 0; i = i - 2)
            {
                try { a[i / 2] = Int16.Parse(str.Substring(i,2),System.Globalization.NumberStyles.HexNumber); }
                catch (Exception)
                { return null; }
            }
            str = string.Join(".",a);
           // Console.WriteLine(str);
            return str;
          
        }

        public string IP_ten(string ip_ten)
        {
            string[] arry =ip_ten.Split(new char[2]{'.',' '});
            if (arry.Length != 4) return null;
            for (int i = 0; i < 4; i++)
            {
                arry[i] = Int16.Parse(arry[i],System.Globalization.NumberStyles.HexNumber).ToString();
            }
          //  Console.WriteLine();
            return string.Join(".",arry);
        }
    }
}
