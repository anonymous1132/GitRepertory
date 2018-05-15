using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace LookUpIp.Classess
{
    public class EDPSqlOper
    {
        public SqlDataAdapter sda;
        public DataTable dt;
        SqlConnectionStringBuilder connbuilder = new SqlConnectionStringBuilder();
        private void connect()
        {
            //sqlconnection
            connbuilder.DataSource = "10.147.218.61";
            connbuilder.IntegratedSecurity = false;
            connbuilder.InitialCatalog = "VRVEIS";
            connbuilder.UserID = "vrv";
            connbuilder.Password = "vrveis";
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


    }
}
