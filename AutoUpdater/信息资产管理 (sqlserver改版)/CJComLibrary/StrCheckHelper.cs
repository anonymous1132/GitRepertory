using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CJComLibrary
{
   public class StrCheckHelper
    {
        public static bool CheckPassword(string password)
        {
            if (password.Length < 8) return false;
            string shuzi = Regex.Replace(password,@"[^0-9]+","");
            string lowlet = Regex.Replace(password, @"[^a-z]+", "");
            string uplet = Regex.Replace(password, @"[^A-Z]+", "");
            if (shuzi.Length > 0 && lowlet.Length > 0 && uplet.Length > 0)
                return true;
            else return false;
        }
    }
}
