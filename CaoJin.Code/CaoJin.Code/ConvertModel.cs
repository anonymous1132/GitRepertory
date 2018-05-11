using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaoJin.Code
{
   public class ConvertModel:IConvertable
    {
         protected string _textboxcontent;
        public string TextBoxContent
        {
            get { return _textboxcontent; }
            set { _textboxcontent = value; }
        }

        protected string _textblockcontent;
        public string TextBlockContent
        {
            get { return _textblockcontent; }

        }

        public void SetTextBlockContentEmpity()
        {
            _textblockcontent = "";
        }

        public virtual void GetTextBlockContent()
        { }

    }
}
