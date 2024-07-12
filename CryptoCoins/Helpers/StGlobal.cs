using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCoins.Helpers
{
    public static class StGlobal
    {
        public static int Id { get; set; }
        public static bool IsAdmin { get; set; }
        public static string EmailId { get; set; }
        public static string password { get; set; }

        public static int Subscription_Id { get; set; }

        //public static MySqlConnection GetSqlConnection()
        //{
        //    // Replace 'your_server_address' with the actual server address provided by Clever Cloud
        //    string serverAddress = "b9qtfiby0glpgdxynecf-mysql.services.clever-cloud.com";
        //    // Replace 'your_database_name' with the actual name of your database
        //    string databaseName = "b9qtfiby0glpgdxynecf";
        //    // Replace 'your_username' with your Clever Cloud database username
        //    string username = "u5h4v1wz0gy4cgq4";
        //    // Replace 'your_password' with your Clever Cloud database password
        //    string password = "q44wqydJFDli59Z6EnxY";

        //    // Construct the connection string
        //    string connectionString = $"Server={serverAddress};Database={databaseName};User Id={username};Password={password};";

        //    MySqlConnection connection = new MySqlConnection(connectionString);
        //    return connection;
        //}

        public static MySqlConnection GetSqlConnection()
        {
            // Replace 'your_server_address' with the actual server address provided by Clever Cloud
            string serverAddress = "bo92wohdakc4hl8oaiwy-mysql.services.clever-cloud.com";
            // Replace 'your_database_name' with the actual name of your database
            string databaseName = "bo92wohdakc4hl8oaiwy";
            // Replace 'your_username' with your Clever Cloud database username
            string username = "ugkclhwbijmbekbn";
            // Replace 'your_password' with your Clever Cloud database password
            string password = "MyCWVYAGF9m17ZMr8nKK";

            // Construct the connection string
            string connectionString = $"Server={serverAddress};Database={databaseName};User Id={username};Password={password};";

            MySqlConnection connection = new MySqlConnection(connectionString);
            return connection;
        }
    }
}
