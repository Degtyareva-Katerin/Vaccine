using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaccine.DB
{
    internal class DataBase
    {
        static SqlConnection sqlConnection = new SqlConnection(@"Data Source = DESKTOP-LI4V3RL;Initial Catalog=vaccine_db;Integrated Security=true");

        public static void openConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }


        public static void closeConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        public static SqlConnection getConnection()
        {
            return sqlConnection;
        }

        public static DataTable selectFromDB(string queryString)
        {
            openConnection();

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand(queryString, getConnection());

            adapter.SelectCommand = cmd;
            adapter.Fill(table);
            
            closeConnection();

            return table;
        }
    }
}
