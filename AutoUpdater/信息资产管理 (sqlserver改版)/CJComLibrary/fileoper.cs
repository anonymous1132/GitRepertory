using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace CJComLibrary
{
   public static class fileoper
    {
       //文件复制
       public static void CjFileCopy(string sourcefile, string destpath)
       {
           if (System.IO.File.Exists(destpath))
           {
               System.IO.File.Delete(destpath);
           }
           System.IO.File.Copy(sourcefile, destpath);
       }

       //文件移动
       public static void CjFileMove(string sourcefile, string destpath)
       {
           if (System.IO.File.Exists(destpath))
           {
               System.IO.File.Delete(destpath);
           }
           System.IO.File.Move(sourcefile, destpath);
       }

        //选择文件
        public static string CjSelectFile(string filter = "Excel(*.xls)|*.xls|Excel(*.xlsx)|*.xlsx|All Files(*.*)|*.*")
            {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = filter;
                openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                openFile.Multiselect = false;
                if (openFile.ShowDialog() == DialogResult.Cancel)
                {
                    return null;
                }

                return openFile.FileName;
            }



    }


}
