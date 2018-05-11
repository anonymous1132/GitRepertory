using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaoJin.Code
{
   public class AsciiConvertHelper
    {
        public static byte[] ConvertToAscii(string sourcestr)
        {
            return Encoding.ASCII.GetBytes(sourcestr);
        }
        public static string ConvertToString(byte[] sourcebyte)
        {
            return Encoding.ASCII.GetString(sourcebyte);
        }
    }
}
