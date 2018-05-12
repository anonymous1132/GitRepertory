using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaoJin.Code
{
    public class UnicodeConvertHelper
    {
        public static byte[] ConvertToBytes(string sourcestr)
        {
            return Encoding.Unicode.GetBytes(sourcestr);
        }
        public static string ConvertToString(byte[] sourcebyte)
        {
            return Encoding.Unicode.GetString(sourcebyte);
        }
    }
}
