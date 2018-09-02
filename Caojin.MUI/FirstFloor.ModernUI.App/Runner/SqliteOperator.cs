using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caojin.Common;
using System.Data;

namespace FirstFloor.ModernUI.App.Runner
{
    public class SqliteOperator
    {
        #region  //ExcuteNonQuery方法

        public static int ExecuteNonQuery(string sql,string database)
        {
            SqliteHelper sqlite = new SqliteHelper();
            sqlite.connectToDatabase(database);
            int result = 0;
            try
            { result= sqlite.modifyTable(sql); }
            catch (Exception)
            { throw; }
            finally
            {
                sqlite.closeDatabase();
            }
            return result;
        }

        public static int[] ExecuteMultiNonQuery(string[]sqls,string database)
        {
            SqliteHelper sqlite = new SqliteHelper();
            sqlite.connectToDatabase(database);
            int[] resault = new int[sqls.Length];
            try
            {
                for (int i = 0; i < sqls.Length; i++)
                {
                   resault[i]= sqlite.modifyTable(sqls[i]);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlite.closeDatabase();
            }
            return resault;
        }
        #endregion

        #region //执行ExcuteReader方法
        public static DataTable ExecuteReader(string sql,string database)
        {
            SqliteHelper sqlite = new SqliteHelper();
            DataTable dt = new DataTable();
            sqlite.connectToDatabase(database);
            try
            {
                 dt = sqlite.Echo(sql);
            }
            catch (Exception)
            { throw; }
            finally
            { sqlite.closeDatabase(); }
            return dt;
        }

        public static DataSet ExecuteMultiReader(string[]sqls,string database)
        {
            SqliteHelper sqlite = new SqliteHelper();
            DataSet ds = new DataSet();
            sqlite.connectToDatabase(database);
            try
            {
                for (int i = 0; i < sqls.Length; i++)
                {
                    ds.Tables.Add(sqlite.Echo(sqls[i]));
                }
            }
            catch (Exception)
            { throw; }
            finally
            { sqlite.closeDatabase(); }
            return ds;
        }
        #endregion
    }
}
