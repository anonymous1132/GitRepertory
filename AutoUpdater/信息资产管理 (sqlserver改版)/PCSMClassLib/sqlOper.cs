using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace PCSMClassLib
{
    //设置sql连接口令
    public  static class GetConnection
    {
        public static string DateSource=xml.DataSource;
        public static bool IntegratedSecurity = false;
        public static string InitialCatalog=xml.InitialCatalog;
        public static string UserID=xml.UserID;
        public static string Password=xml.Password;

    }
     public  class sqlOper
    {
        public SqlDataAdapter sda;
        public DataTable dt;
        SqlConnectionStringBuilder connbuilder = new SqlConnectionStringBuilder();
        private void connect()
        {
         
            //sqlconnection
            connbuilder.DataSource =GetConnection.DateSource;
            connbuilder.IntegratedSecurity = GetConnection.IntegratedSecurity;
            connbuilder.InitialCatalog = GetConnection.InitialCatalog;
            connbuilder.UserID =GetConnection.UserID;
            connbuilder.Password = GetConnection.Password;
           // connbuilder.ConnectTimeout = 3000;
        }

        
       public void getSomeDate(string sql)
       {
           connect();
           SqlConnection conn = new SqlConnection(connbuilder.ConnectionString);
           sda = new SqlDataAdapter(sql, conn);
           this.dt = new DataTable();
           this.sda.AcceptChangesDuringUpdate = true;
            this.sda.Fill(dt);
            conn.Close();
       }
       public void getSomeDate(string sql, ref string outmessage)
       {
           try { getSomeDate(sql); }
           catch(Exception e)
           {
               outmessage = e.ToString();
           }
       }

    }

     public class accessOper
     {
         public DataTable dt;
         public void getSomeData(string sql)
         {
             string strConnection = "Provider = Microsoft.ACE.OLEDB.12.0;";
             strConnection += @"Data Source =data\test.accdb;Jet OLEDB:Database Password=black5408";
             using (OleDbConnection objConnection = new OleDbConnection(strConnection))
             {
                 objConnection.Open();
                 OleDbDataAdapter oda = new OleDbDataAdapter(sql, objConnection);
                 this.dt = new DataTable();
                 oda.Fill(dt);
                 objConnection.Close();
                 
             }
         
         }
     
     }

}
