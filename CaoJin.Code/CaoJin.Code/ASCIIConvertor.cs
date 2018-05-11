using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Data;
using System.Diagnostics;
using System.Collections;

namespace CaoJin.Code
{
   public class Sting2ASCIIConvertor:ConvertModel
    {

        public  override void GetTextBlockContent()
        {
            if (string.IsNullOrEmpty(_textboxcontent))
            { throw new TextBoxInputException("输入为空"); }
           
                byte[] b = AsciiConvertHelper.ConvertToAscii(TextBoxContent);
                foreach (byte lb in b)
                {
                    _textblockcontent = _textblockcontent + lb.ToString() + " ";
                }
            _textblockcontent = _textblockcontent.Trim();
        }
    }
}
