using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections;
using System.Data.SqlClient;
using System.Reflection;
using System.IO;

namespace CJComLibrary
{
   public  class Exceloper
    {
        //打开多个文件，每个文件读取一个sheet，返回dataset
        public DataSet ExcelToDS(string[]filepath,bool hasTitle = false,string sheetname="Sheet1")
        {   
            string strCon = "";
            string strCom = "";
            using (DataSet ds = new DataSet())
            {
                for (int i = 0; i < filepath.Length; i++)
                {
                    strCon = getstrCon(filepath[i],  hasTitle);
                    strCom =string.Format(" SELECT * FROM [{0}$]",sheetname);
                    try
                    {
                        using (OleDbConnection myConn = new OleDbConnection(strCon))
                        using (OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, strCon))
                        {
                            myConn.Open();
                            myCommand.Fill(ds, i.ToString());
                        }
                    }
                    catch (Exception)
                    {
                        ds.Tables.Add(i.ToString());
                    }
                }

                if (ds == null || ds.Tables.Count <= 0) return null;
                return ds;
            }
        }

        //打开一个文件，读取一个sheet页，返回datatable
        public DataTable ExcelToDT(string filepath,bool hasTitle=false,string sheetname="Sheet1")
        {
            string strCon = "";
            string strCom = "";
            using (DataSet ds = new DataSet())
            {
                strCon = getstrCon(filepath,  hasTitle);
                strCom = string.Format(" SELECT * FROM [{0}$]", sheetname);
                try
                {
                    using (OleDbConnection myConn = new OleDbConnection(strCon))
                    using (OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, strCon))
                    {
                        myConn.Open();
                        myCommand.Fill(ds, sheetname);
                    }
                }
                catch (Exception)
                {
                    return null;
                }
                if (ds == null || ds.Tables.Count <= 0) return null;
                return ds.Tables[sheetname];
            }
        }

       //构造连接excel字符串
        private string getstrCon(string filepath,bool hasTitle)
        {
            string filetype = Path.GetExtension(filepath);
            return string.Format("Provider=Microsoft.{4}.OLEDB.{0}.0;" +
                                   "Extended Properties=\"Excel {1}.0;HDR={2};IMEX=1;\";" +
                                   "data source={3};",
                                   (filetype == ".xls" ? 4 : 12), (filetype == ".xls" ? 8 : 12), (hasTitle ? "Yes" : "NO"), filepath, (filetype == ".xls" ? "Jet" : "ACE"));
        }

        //打开一个文件，读取文件内所有sheet页，返回dataset
        public DataSet ExcelToDS(string filepath,bool hasTitle=false)
        {
            string strCon = "";
            string strCom = "";
            using (DataSet ds = new DataSet())
            {
                strCon = getstrCon(filepath,  hasTitle);
                try
                {
                    using (OleDbConnection myConn = new OleDbConnection(strCon))
                    {
                            myConn.Open();
                            DataTable sheetNames = myConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                            foreach (DataRow dr in sheetNames.Rows)
                            {
                                strCom = string.Format(" SELECT * FROM [{0}]", dr[2].ToString());
                                using (OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, strCon))
                                {
                                    myCommand.Fill(ds, dr[2].ToString());
                                }
                            }
                        
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    return null;
                }
                if (ds == null || ds.Tables.Count <= 0) return null;
                return ds;
            }
        }

        //查找datatable中的字符串首次在第几行
        public int rowindex(DataTable dt, string str)
        {
            DataView dv = dt.DefaultView;
            foreach (DataRowView drv in dv)
            {
                if (drv[0].ToString() == str)
                { return dt.Rows.IndexOf(drv.Row); }

            }
            return 0;
        }

       //dt导出至excel
        public void ExExcel(DataTable dt, string path)
        {
            if (dt == null || dt.Rows.Count == 0) return;
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null) return;
            System.Globalization.CultureInfo CurrentCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];
            Microsoft.Office.Interop.Excel.Range range;
            long totalCount = dt.Rows.Count;
            long rowRead = 0;
            float percent = 0;

            for (int r = 0; r < totalCount; r++)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cells[r + 2, i + 1] = dt.Rows[r][i].ToString();

                }
                rowRead++;
                percent = ((float)(100 * rowRead)) / totalCount;
            }
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dt.Columns[i].ColumnName;
                range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, i + 1];
                range.Interior.ColorIndex = 15;
                range.Font.Bold = true;
                range.EntireColumn.AutoFit();
            }
            // xlApp.Visible = true;
            workbook.Saved = true;
            workbook.SaveCopyAs(path);
            workbooks.Close();
            Kill(xlApp);
        }

        //不含标题
        public void ExExcel2(DataTable dt, string path ,string sheetname="")
        {
            if (dt == null || dt.Rows.Count == 0) return;
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null) return;
            System.Globalization.CultureInfo CurrentCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(path);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];

            sheetname = sheetname.Replace("?","");
            sheetname = sheetname.Replace("[", "");
            sheetname = sheetname.Replace("]", "");
            sheetname = sheetname.Replace("/", "");
            sheetname = sheetname.Replace("\\", "");
            sheetname = sheetname.Replace("*", "");
            sheetname = sheetname.Replace("？", "");
            if (sheetname.Length > 31)
            { sheetname = sheetname.Substring(0,31); }
            if(sheetname!="")
            {
                
                worksheet.Name = sheetname; 
            }

            long totalCount = dt.Rows.Count;
            long rowRead = 0;

            for (int r = 0; r < totalCount; r++)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Rows[r][i].ToString() != "")
                    {
                        worksheet.Cells[r + 1, i + 1] = dt.Rows[r][i].ToString();
                    }
                }
                rowRead++;
            }
            workbook.Saved = true;
            workbook.SaveCopyAs(path);
            workbooks.Close();
            Kill(xlApp);
        }

        #region   杀死束Excel进程
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);
        public static void Kill(Microsoft.Office.Interop.Excel.Application excel)
        {
            IntPtr t = new IntPtr(excel.Hwnd);   //得到这个句柄，具体作用是得到这块内存入口   

            int k = 0;
            GetWindowThreadProcessId(t, out k);   //得到本进程唯一标志k  
            System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(k);   //得到对进程k的引用  
            p.Kill();     //关闭进程k  

        }
        #endregion
        
        //public void upload(DataTable dt,out string resault,Dictionary<string,string>dic)
        //{
        //    resault="";
        //    if (dt.Rows[1][5].ToString() == "")
        //    {
        //        resault = "请检查工号是否为空！";
        //        return;
        //    }
        //    if (!dic.ContainsKey(User.range))
        //    {
        //        resault = "用户类型错误！";
        //        return;
        //    }
        //    if (User.range == "all")
        //    {
        //        if (!dic.ContainsValue(dt.Rows[2][5].ToString()))
        //        {
        //            resault = "请检查单位名称是否规范！";
        //            return;
        //        }
        //    }
        //    else if (dic[User.range] != dt.Rows[2][5].ToString() )
        //    {
        //        resault = "禁止上传其他单位或员工信息或单位名称不规范！";
        //        return;
        //    }
          
        //    sqlOper sqo = new sqlOper();
        //    string  gonghao =dt.Rows[1][5].ToString();
        //    string  name = dt.Rows[1][1].ToString();
        //    string DanWei = dt.Rows[2][5].ToString();
        //    string  birthday =dt.Rows[3][1].ToString();
        //    string  workingday =dt.Rows[4][5].ToString();
        //    string   tuanzhibu =dt.Rows[3][5].ToString();
        //    string   JiGuan =dt.Rows[5][1].ToString();
        //    string   sex =dt.Rows[2][1].ToString();
        //    string   ZhengZiMianMao =dt.Rows[6][1].ToString();
        //    string   XueLi =dt.Rows[6][5].ToString();
        //    string   school =dt.Rows[5][5].ToString();
        //    string   schoolZhuanYe = dt.Rows[7][5].ToString();
        //    string minzu = dt.Rows[4][1].ToString();
        //    string interest = dt.Rows[8][5].ToString();
        //    string goodat = dt.Rows[8][1].ToString();
        //    string XingGe = dt.Rows[7][1].ToString();
            
        //    int rd1 = rowindex(dt, "专业技术资格");
        //    int rd0 = rowindex(dt, "工作经历");

        //    string ZhuanYZG = (dt.Rows[rd1+1][1].ToString() == "" ? "暂无" : dt.Rows[rd1+1][1].ToString()) + " " + (dt.Rows[rd1+1][2].ToString() == "" ? "暂无" : dt.Rows[rd1+1][2].ToString());
           
           
        //    //工作经历
        //    int len_exp = rd1 - rd0 - 1;
        //    string[]years_exp=new string[len_exp];
        //    string[] danwei_exp = new string[len_exp];
        //    string[] banzu_exp = new string[len_exp];
        //    string[] gangwei_exp = new string[len_exp];
        //    for (int i = 0; i < len_exp; i++)
        //    {
        //        years_exp[i] = dt.Rows[i + rd0+1][1].ToString();
        //        danwei_exp[i] = dt.Rows[i + rd0+1][2].ToString();
        //        banzu_exp[i] = dt.Rows[i + rd0+1][4].ToString();
        //        gangwei_exp[i] = dt.Rows[i + rd0+1][6].ToString();
        //    }

        //    rd0 = rowindex(dt, "职业技能资格");
        //    string ZhiYZG = (dt.Rows[rd0+1][1].ToString() == "" ? "暂无" : dt.Rows[rd0+1][1].ToString()) + " " + (dt.Rows[rd0+1][2].ToString() == "" ? "暂无" : dt.Rows[rd0+1][2].ToString());

        //    rd0 = rowindex(dt, "Ⅰ.工作业绩");
        //    rd1 = rowindex(dt, "Ⅱ.专业业绩");
            
        //    //工作业绩
        //    int len_gzyj = rd1-rd0-2;
        //    string[] years_gzyj=new string[len_gzyj];
        //    string[] content_gzyj = new string[len_gzyj];
        //    float[] self_gzyj = new float[len_gzyj];
        //    float[] hr_gzyj = new float[len_gzyj];
        //    string[] hry_gzyj = new string[len_gzyj];
        //    for (int i = 0; i < len_gzyj; i++)
        //    {
        //        years_gzyj[i] = dt.Rows[i + rd0+1][1].ToString();
        //        content_gzyj[i]=dt.Rows[i+rd0+1][2].ToString();
        //        self_gzyj[i] =(dt.Rows[i+rd0+1][4].ToString()==""?0:Convert.ToSingle(dt.Rows[i + rd0+1][4]));
        //        hr_gzyj[i] =(dt.Rows[i + rd0+1][5].ToString()==""?0:Convert.ToSingle(dt.Rows[i + rd0+1][5]));
        //        hry_gzyj[i] = dt.Rows[i + rd0+1][6].ToString();
                
        //    }

        //    rd0 = rowindex(dt, "Ⅲ.创新业绩");
        //    //专业业绩
        //    int len_zyyj = rd0-rd1-2;
        //    string[] years_zyyj = new string[len_zyyj];
        //    string[] content_zyyj = new string[len_zyyj];
        //    float[] self_zyyj = new float[len_zyyj];
        //    float[] hr_zyyj = new float[len_zyyj];
        //    string[] hry_zyyj = new string[len_zyyj];
        //    string[] level_zyyj = new string[len_zyyj];
        //    for (int i = 0; i < len_zyyj; i++)
        //    {
        //        years_zyyj[i] = dt.Rows[i + rd1+1][1].ToString();
        //        content_zyyj[i] = dt.Rows[i + rd1+1][2].ToString();
        //        self_zyyj[i] = (dt.Rows[i + rd1+1][4].ToString()==""?0:Convert.ToSingle(dt.Rows[i + rd1+1][4]));
        //        hr_zyyj[i] = (dt.Rows[i + rd1+1][5].ToString()==""?0:Convert.ToSingle(dt.Rows[i + rd1+1][5]));
        //        hry_zyyj[i] = dt.Rows[i + rd1+1][6].ToString();
        //        level_zyyj[i] = dt.Rows[i + rd1+1][3].ToString();
        //    }

        //    rd1 = rowindex(dt, "Ⅳ.学习业绩");          
        //    //创新业绩
        //    int len_cxyj = rd1-rd0-2;
        //    string[] years_cxyj = new string[len_cxyj];
        //    string[] content_cxyj = new string[len_cxyj];
        //    float[] self_cxyj = new float[len_cxyj];
        //    float[] hr_cxyj = new float[len_cxyj];
        //    string[] hry_cxyj = new string[len_cxyj];
        //    string[] level_cxyj = new string[len_cxyj];
        //    for (int i = 0; i < len_cxyj; i++)
        //    {
        //        years_cxyj[i] = dt.Rows[i + rd0+1][1].ToString();
        //        content_cxyj[i] = dt.Rows[i + rd0+1][2].ToString();
        //        self_cxyj[i] =(dt.Rows[i + rd0+1][4].ToString()==""?0: Convert.ToSingle(dt.Rows[i +rd0+1][4]));
        //        hr_cxyj[i] =(dt.Rows[i + rd0+1][5].ToString()==""?0:Convert.ToSingle(dt.Rows[i + rd0+1][5]));
        //        hry_cxyj[i] = dt.Rows[i + rd0+1][6].ToString();
        //        level_cxyj[i] = dt.Rows[i + rd0+1][3].ToString();
        //    }
        //    rd0 = rowindex(dt, "Ⅴ.工团业绩");   
        //    //学习业绩
        //    int len_xxyj = rd0-rd1-2;
        //    string[] years_xxyj = new string[len_xxyj];
        //    string[] content_xxyj = new string[len_xxyj];
        //    int[] self_xxyj = new int[len_xxyj];
        //    int[] hr_xxyj = new int[len_xxyj];
        //    string[] hry_xxyj = new string[len_xxyj];
        //    for (int i = 0; i < len_xxyj; i++)
        //    {
        //        years_xxyj[i] = dt.Rows[i + rd1+1][1].ToString();
        //        content_xxyj[i] = dt.Rows[i + rd1+1][2].ToString();
        //        self_xxyj[i] = (dt.Rows[i + rd1+1][4].ToString()==""?0:Convert.ToInt32(dt.Rows[i + rd1+1][4]));
        //        hr_xxyj[i] = (dt.Rows[i + rd1+1][5].ToString()==""?0:Convert.ToInt32(dt.Rows[i + rd1+1][5]));
        //        hry_xxyj[i] = dt.Rows[i + rd1+1][6].ToString();
        //    }
        //    rd1 = rowindex(dt, "Ⅵ.荣誉称号");
        //    //工团业绩
        //    int len_gtyj = rd1-rd0-2;
        //    string[] years_gtyj = new string[len_gtyj];
        //    string[] content_gtyj = new string[len_gtyj];
        //    int[] self_gtyj = new int[len_gtyj];
        //    int[] hr_gtyj = new int[len_gtyj];
        //    string[] hry_gtyj = new string[len_gtyj];
        //    for (int i = 0; i < len_gtyj; i++)
        //    {
        //        years_gtyj[i] = dt.Rows[i + rd0+1][1].ToString();
        //        content_gtyj[i] = dt.Rows[i + rd0+1][2].ToString();
        //        self_gtyj[i] =(dt.Rows[i + rd0+1][4].ToString()==""?0:Convert.ToInt32(dt.Rows[i + rd0+1][4]));
        //        hr_gtyj[i] = (dt.Rows[i + rd0+1][5].ToString()==""?0:Convert.ToInt32(dt.Rows[i + rd0+1][5]));
        //        hry_gtyj[i] = dt.Rows[i + rd0+1][6].ToString();
        //    }
        //    rd0 = rowindex(dt, "业绩总分");
        //    //荣誉称号
        //    int len_rych =rd0-rd1-2;
        //    string[] years_rych = new string[len_rych];
        //    string[] content_rych = new string[len_rych];
        //    int[] self_rych = new int[len_rych];
        //    int[] hr_rych = new int[len_rych];
        //    string[] hry_rych = new string[len_rych];
        //    string[] level_rych = new string[len_rych];
        //    for (int i = 0; i < len_rych; i++)
        //    {
        //        years_rych[i] = dt.Rows[i + rd1+1][1].ToString();
        //        content_rych[i] = dt.Rows[i + rd1+1][2].ToString();
        //        self_rych[i] =(dt.Rows[i + rd1+1][4].ToString()==""?0:Convert.ToInt32(dt.Rows[i + rd1+1][4]));
        //        hr_rych[i] = (dt.Rows[i + rd1+1][5].ToString()==""?0:Convert.ToInt32(dt.Rows[i + rd1+1][5]));
        //        hry_rych[i] = dt.Rows[i + rd1+1][6].ToString();
        //        level_rych[i] = dt.Rows[i + rd1+1][3].ToString();
        //    }

        //    rd0 = rowindex(dt, "年度个人总结");
        //    //第三部分
        //    string zongjie = dt.Rows[rd0+1][0].ToString();
        ////    MessageBox.Show(zongjie.Length.ToString());
         
        //    string sql = "select * from pinggushow where id ='" + gonghao + "'";
        //    sqo.getSomeDate(sql);
        //    if (sqo.dt.Rows.Count > 0)
        //    {
        //        //删除原数据
        //        sql = "delete from pinggushow where id='" + gonghao + "'; delete from pinggushow_gzyj where id ='" + gonghao + "';delete from pinggushow_zyyj where id ='" + gonghao + "';delete from pinggushow_cxyj where id ='" + gonghao + "'; delete from pinggushow_xxyj where id ='" + gonghao + "';delete from pinggushow_rych where id ='" + gonghao + "';delete from pinggushow_gtyj where id ='" + gonghao + "';delete from pinggushowpart3 where id ='" + gonghao + "';delete from pinggushow_experience where id ='" + gonghao + "'";
        //        sqo.getSomeDate(sql);
        //    }
            
        //        //上传数据库
        //        sql = "insert into pinggushow (name,DanWei,birthday,firstWorkingDay,JiGuan,ZhenZiMianMao,XueLi,school,schoolZhuanYe,ZhuanYZG,ZhiYZG,id,MinZu,Interest,goodat,XingGe,sex,TuanZhiBu) values ('" + name + "','" + DanWei + "','" + birthday + "','" + workingday + "','" + JiGuan + "','" + ZhengZiMianMao + "','" + XueLi + "','" + school + "','" + schoolZhuanYe + "','" + ZhuanYZG + "','" + ZhiYZG + "','" + gonghao + "','" + minzu + "','" + interest + "','" + goodat + "','" + XingGe + "','" + sex + "','" + tuanzhibu + "')";
        //        sqo.getSomeDate(sql);
        //        sql = "insert into pinggushowPart3 (id,textContent) values ('" + gonghao + "','" + zongjie + "')";
        //        sqo.getSomeDate(sql);
        //        for (int i = 0; i < len_exp; i++)
        //        {
        //            if (years_exp[i] + danwei_exp[i] == "") continue;
                    
        //                sql = "insert into pinggushow_experience (id,Years,DanWei,BanZu,GangWei) values ('" + gonghao + "','" + years_exp[i] + "','" + danwei_exp[i] + "','" + banzu_exp[i] + "','" + gangwei_exp[i] + "')";
        //                sqo.getSomeDate(sql);
                    
        //        }
        //        for (int i = 0; i < len_gzyj; i++)
        //        {
        //            if (content_gzyj[i] == "") continue;
        //            sql = "insert into pinggushow_gzyj (id,Years,Contents,selfPing,HRPing,HRY) values ('" + gonghao + "','" + years_gzyj[i] + "','" + content_gzyj[i] + "','" + self_gzyj[i] + "','" + hr_gzyj[i] + "','" + hry_gzyj[i] + "')";
        //            sqo.getSomeDate(sql);
        //        }
        //        for (int i = 0; i < len_xxyj; i++)
        //        {
        //            if (content_xxyj[i] == "") continue;
        //            sql = "insert into pinggushow_xxyj (id,Years,Contents,selfPing,HRPing,HRY) values ('" + gonghao + "','" + years_xxyj[i] + "','" + content_xxyj[i] + "','" + self_xxyj[i] + "','" + hr_xxyj[i] + "','" + hry_xxyj[i] + "')";
        //            sqo.getSomeDate(sql);
        //        }
        //        for (int i = 0; i < len_gtyj; i++)
        //        {
        //            if (content_gtyj[i] == "") continue;
        //            sql = "insert into pinggushow_gtyj (id,Years,Contents,selfPing,HRPing,HRY) values ('" + gonghao + "','" + years_gtyj[i] + "','" + content_gtyj[i] + "','" + self_gtyj[i] + "','" + hr_gtyj[i] + "','" + hry_gtyj[i] + "')";
        //            sqo.getSomeDate(sql);
        //        }
        //        for (int i = 0; i < len_zyyj; i++)
        //        {
        //            if (content_zyyj[i] == "") continue;
        //            sql = "insert into pinggushow_zyyj (id,Years,Contents,selfPing,HRPing,HRY,Lev) values ('" + gonghao + "','" + years_zyyj[i] + "','" + content_zyyj[i] + "','" + self_zyyj[i] + "','" + hr_zyyj[i] + "','" + hry_zyyj[i] + "','" + level_zyyj[i] + "')";
        //            sqo.getSomeDate(sql);
        //        }
        //        for (int i = 0; i < len_cxyj; i++)
        //        {
        //            if (content_cxyj[i] == "") continue;
        //            sql = "insert into pinggushow_cxyj (id,Years,Contents,selfPing,HRPing,HRY,Lev) values ('" + gonghao + "','" + years_cxyj[i] + "','" + content_cxyj[i] + "','" + self_cxyj[i] + "','" + hr_cxyj[i] + "','" + hry_cxyj[i] + "','" + level_cxyj[i] + "')";
        //            sqo.getSomeDate(sql);
        //        }
        //        for (int i = 0; i < len_rych; i++)
        //        {
        //            if (content_rych[i] == "") continue;
        //            sql = "insert into pinggushow_rych (id,Years,Contents,selfPing,HRPing,HRY,Lev) values ('" + gonghao + "','" + years_rych[i] + "','" + content_rych[i] + "','" + self_rych[i] + "','" + hr_rych[i] + "','" + hry_rych[i] + "','" + level_rych[i] + "')";
        //            sqo.getSomeDate(sql);
        //        }
            
        //    //总分
        //        sql = "update pinggushow set zongfen = (select zongfen_gzyj + zongfen_zyyj + zongfen_cxyj + zongfen_xxyj + zongfen_gtyj + zongfen_rych as zf from pinggushow where id ='" + gonghao + "') where id='" + gonghao + "'";
        //        sqo.getSomeDate(sql);
        //        resault = gonghao + name +"上传或更新成功！";
        //}

        //public void uploadxgcp(DataTable dt)
        //{
        //    string sql = "";
        //    sqlOper sqo = new sqlOper();
        //    DataView dv = dt.DefaultView;
        //    foreach (DataRowView drv in dv)
        //    {
        //        if (drv[1].ToString() != "性格测试分析")
        //        {
        //            sql = "update pinggushowPart3 set xinggeceshi = '" + drv[1].ToString() + "' where id = '" + drv[0].ToString() + "'";
        //            sqo.getSomeDate(sql);
        //        }
            
        //    }
        //}

        //public void uploadpersonalguid(DataTable dt,out string resault,Dictionary<string,string>dic)
        //{
        //    resault = "";
        //    string name = dt.Rows[1][1].ToString();
        //    string gonghao = dt.Rows[1][5].ToString();
        //    string sex = dt.Rows[2][1].ToString();
        //    string danwei = dt.Rows[2][5].ToString();
        //    if (gonghao == "")
        //    {
        //        resault = "请检查工号是否为空！";
        //        return;
        //    }
        //    if (!dic.ContainsKey(User.range))
        //    {
        //        resault = "用户类型错误！";
        //        return;
        //    }
        //    if (User.range == "all" )
        //    {
        //        if (!dic.ContainsValue(danwei))
        //        {
        //            resault = "请检查单位名称是否规范！";
        //            return;
        //        }
        //    }
        //    else if (dic[User.range] != danwei)
        //    {
        //        resault = "禁止上传其他单位或员工信息或单位名称不规范！";
        //        return;
        //    }


        //    string birthday = dt.Rows[3][1].ToString();
        //    string workday=dt.Rows[3][5].ToString();
        //    string jiguan = dt.Rows[4][1].ToString();
        //    string xueli = dt.Rows[4][5].ToString();
        //    string zzmm = dt.Rows[5][1].ToString();
        //    string school=dt.Rows[5][5].ToString();
        //    string xingge = dt.Rows[6][1].ToString();
        //    string schoolzhuanye=dt.Rows[6][5].ToString();
        //    string goodat = dt.Rows[7][1].ToString();
        //    string interest = dt.Rows[7][5].ToString();
        //    string zyzg=dt.Rows[8][1].ToString();
        //    string zhiyzg = dt.Rows[8][5].ToString();
        //    string position = dt.Rows[9][7].ToString()==""?"":dt.Rows[9][7].ToString().ToUpper().Substring(0,1);
        //    string best = dt.Rows[10][7].ToString() == "" ? "" : dt.Rows[10][7].ToString().ToUpper();
        //    string best2 = dt.Rows[10][8].ToString() == "" ? "" : dt.Rows[10][8].ToString().ToUpper().Substring(0,1);
        //    if (best.Length > 1)
        //    {
        //        best = best.ToUpper().Substring(0, 1) + " " + best.ToUpper().Substring(1, 1);
        //    }
        //    else {
        //        best = best + " " + best2;
        //    }
        //    string fazhanguid = dt.Rows[11][7].ToString() == "" ? "" : dt.Rows[11][7].ToString().ToUpper().Substring(0, 1);
        //    string fazhanguidf = fazhanguid=="E"?dt.Rows[11][8].ToString():"";

        //    //2017
        //    int rd0 = rowindex(dt, "2017年");
        //    int rd1 = rowindex(dt, "2018年");
        //    int len_2017 = rd1-rd0-1;
          
        //    string[] yjlx_2017 = new string[len_2017];
        //    string[] target_2017 = new string[len_2017];
        //    string[] steps_2017 = new string[len_2017];
        //    string[] finishedtime_2017 = new string[len_2017];
        //    for (int i = 0; i < len_2017; i++)
        //    {
        //        yjlx_2017[i] = dt.Rows[i+rd0+1][1].ToString();
        //        target_2017[i] = dt.Rows[i+rd0+1][2].ToString();
        //        steps_2017[i] = dt.Rows[i+rd0+1][3].ToString();
        //        finishedtime_2017[i] = dt.Rows[i+rd0+1][4].ToString();
        //    }
        //   //2018年
        //    rd0 = rowindex(dt, "2019年");
        //    int len_2018 = rd0-rd1-1; 
        //    string[] yjlx_2018 = new string[len_2018];
        //    string[] target_2018 = new string[len_2018];
        //    string[] steps_2018 = new string[len_2018];
        //    string[] finishedtime_2018 = new string[len_2018];
        //    for (int i = 0; i < len_2018; i++)
        //    {
        //        yjlx_2018[i] = dt.Rows[i+rd1+1][1].ToString();
        //        target_2018[i] = dt.Rows[i+rd1+1][2].ToString();
        //        steps_2018[i] = dt.Rows[i+rd1+1][3].ToString();
        //        finishedtime_2018[i] = dt.Rows[i+rd1+1][4].ToString();
        //    }

        //    //2019年
        //    rd1 = rowindex(dt, "其他");
        //    int len_2019 = rd1-rd0-1;
        //    string[] yjlx_2019 = new string[len_2019];
        //    string[] target_2019 = new string[len_2019];
        //    string[] steps_2019 = new string[len_2019];
        //    string[] finishedtime_2019 = new string[len_2019];
        //    for (int i = 0; i < len_2019; i++)
        //    {
        //        yjlx_2019[i] = dt.Rows[i+rd0+1][1].ToString();
        //        target_2019[i] = dt.Rows[i+rd0+1][2].ToString();
        //        steps_2019[i] = dt.Rows[i+rd0+1][3].ToString();
        //        finishedtime_2019[i] = dt.Rows[i+rd0+1][4].ToString();
        //    }
        //    //其他
        //    string others = dt.Rows[rd1][1].ToString();
        //    //总分
        //    float zongfen = 0;
        //    try
        //    { 
        //        zongfen = Convert.ToSingle(dt.Rows[12][7]);
        //        resault = gonghao + name + "上传或更新成功！";
        //    }
        //    catch (Exception)
        //    { zongfen = 0;
        //    resault =  "已上传，总分一栏转换失败，强制设为0！";
        //    }
        //    finally
        //    { 
        //    // 执行导入数据
        //        sqlOper sqo = new sqlOper();
        //        string sql = "select * from pinggushow_personalguide where id ='" + gonghao + "'";
        //        sqo.getSomeDate(sql);
        //        if (sqo.dt.Rows.Count > 0)
        //        {
        //            //删除原数据
        //            sql = "delete from pinggushow_personalguide where id='" + gonghao + "'; delete from pinggushow_personalguide_plan where id ='" + gonghao + "'";
        //            sqo.getSomeDate(sql);
        //        }
        //        sql = "insert into pinggushow_personalguide  (id,name,sex,DanWei,Birthday,firstWorkingday,JiGuan,XueLi,ZhenZhiMianMao,school,XingGe,schoolZhuanYe,goodat,Interest,zyzg,zhiyzg,wantedposition,bestablity,fazhanguihua,fazhanguihua2,zongfen,others) values ('" + gonghao + "','" + name + "','" + sex + "','" + danwei + "','" + birthday + "','" + workday + "','" + jiguan + "','" + xueli + "','" + zzmm + "','" + school + "','" + xingge + "','" + schoolzhuanye + "','" + goodat + "','" + interest + "','" + zyzg + "','" + zhiyzg + "','" + position + "','" + best + "','" + fazhanguid + "','" + fazhanguidf + "','" + zongfen + "','" + others + "')";
        //        sqo.getSomeDate(sql);
        //    //2017
        //        for (int i = 0; i < len_2017; i++)
        //        {

        //            if (yjlx_2017[i] + target_2017[i] == "") continue;
        //            sql = "insert into pinggushow_personalguide_plan (id,year,yjlx,target,steps,finishedtime) values ('" + gonghao + "','2017','" + yjlx_2017[i] + "','" + target_2017[i] + "','" + steps_2017[i] + "','" + finishedtime_2017[i] + "')";
        //            sqo.getSomeDate(sql);
        //        }

        //        //2018

        //        for (int i = 0; i < len_2018; i++)
        //        {
        //            if (yjlx_2018[i] + target_2018[i] == "") continue;
        //            sql = "insert into pinggushow_personalguide_plan (id,year,yjlx,target,steps,finishedtime) values ('" + gonghao + "','2018','" + yjlx_2018[i] + "','" + target_2018[i] + "','" + steps_2018[i] + "','" + finishedtime_2018[i] + "')";
        //            sqo.getSomeDate(sql);
        //        }

        //        //2019
        //        for (int i = 0; i < len_2019; i++)
        //        {
        //            if (yjlx_2019[i] + target_2019[i] == "") continue;
        //            sql = "insert into pinggushow_personalguide_plan (id,year,yjlx,target,steps,finishedtime) values ('" + gonghao + "','2019','" + yjlx_2019[i] + "','" + target_2019[i] + "','" + steps_2019[i] + "','" + finishedtime_2019[i] + "')";
        //            sqo.getSomeDate(sql);
        //        }
                
        //    }


        //}

    }
}
