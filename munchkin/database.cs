using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace munchkin
{
    internal class database
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;database=cards;uid=root;pwd=root;charset=utf8mb4;");

        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            { 
                connection.Open();
            }
        }
        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public MySqlConnection getConnection()
        {
            return connection;
        }
    }
}
