using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace CJComLibrary
{
  
     public class AccessOper
     {
         public DataTable dt;
         public void getSomeData(string sql,string strConnection)
         {
             //以下连接符
             //string strConnection = "Provider = Microsoft.ACE.OLEDB.12.0;";
             //strConnection += @"Data Source =data\test.accdb;Jet OLEDB:Database Password=black5408";
             using (OleDbConnection objConnection = new OleDbConnection(strConnection))
             {
                 objConnection.Open();
                 OleDbDataAdapter oda = new OleDbDataAdapter(sql, objConnection);
                     this.dt = new DataTable();
                     oda.Fill(dt);
                     objConnection.Close();
                 
             } 
         
         }

         public void UpdateSomeData(string sql, string strConnection)
         { 
          using (OleDbConnection objConnection = new OleDbConnection(strConnection))
             {
                 objConnection.Open();
                 OleDbCommand cmd=new OleDbCommand(sql,objConnection);
                 OleDbDataReader reader = cmd.ExecuteReader();
       
                 
             } 
         
         }
         

     }

}
