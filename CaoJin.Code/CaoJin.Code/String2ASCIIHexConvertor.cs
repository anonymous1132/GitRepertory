using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaoJin.Code
{
    public class String2ASCIIHexConvertor:ConvertModel
    {
        public override void GetTextBlockContent()
        {
            if (string.IsNullOrEmpty(_textboxcontent))
            { throw new TextBoxInputException("输入为空"); }

            byte[] b = AsciiConvertHelper.ConvertToBytes(TextBoxContent);
            foreach (byte lb in b)
            {

                _textblockcontent = _textblockcontent + Convert.ToString(lb,16) + " ";
            }
            _textblockcontent = _textblockcontent.Trim();
        }
    }
}
