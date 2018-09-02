using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;

namespace Caojin.Common
{
   public class SqliteHelper
    {
        SQLiteConnection m_dbConnection;
        public SqliteHelper()
        { }
        //创建一个空的数据库
       public void createNewDatabase(string database)
        {
            SQLiteConnection.CreateFile(database);
        }

        //创建一个连接到指定数据库
       public void connectToDatabase(string database)
        {
            m_dbConnection = new SQLiteConnection("Data Source="+database+";Version=3;");
            m_dbConnection.Open();
        }

        public void closeDatabase()
        {
            m_dbConnection.Dispose();
        }

        //在指定数据库中创建一个table 形如highscores (name varchar(20), score int)
        public  void createTable(string table)
        {
            string sql = "create table "+table;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        //修改一些数据，如insert，update
        public  int modifyTable(string sql)
        {
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            return command.ExecuteNonQuery();
            
        }

        //使用sql查询语句，并显示结果
        public  DataTable Echo(string sql)
        {
           // string sql = "select * from highscores order by score desc";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.FieldCount <= 0) return null;
            DataTable dt = new DataTable();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                dt.Columns.Add(reader.GetName(i));
            }

            while (reader.Read())
            {
                DataRow dr = dt.NewRow();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    dr[i] = reader[i];
                }
                dt.Rows.Add(dr);
             }
            return dt;
        
        }

    }
}
