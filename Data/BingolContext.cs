using Microsoft.Data.SqlClient;
using System.Data;

namespace Bingol.Data
{
    public static class BingolContext
    {
        private const string ServerName = "localhost";
        private const string Database = "bingol";
        private static readonly string ConnectionString = $"Data Source={ServerName};Initial Catalog={Database};Integrated Security=True";

        public static DataTable GetArray(string query)
        {
            var dataTable = new DataTable();
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            var sqlData = new SqlDataAdapter(query, connection);
            sqlData.Fill(dataTable);
            connection.Close();
            return dataTable;
        }
        public static void Execute(string query)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            var command = new SqlCommand(query, connection);
            command.ExecuteReader();
            connection.Close();
        }
    }
}
