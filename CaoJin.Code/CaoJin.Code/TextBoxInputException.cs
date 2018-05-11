using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaoJin.Code
{
   public class TextBoxInputException:ApplicationException
    {
        public TextBoxInputException(string msg) : base(msg)
        { }
    }
}
