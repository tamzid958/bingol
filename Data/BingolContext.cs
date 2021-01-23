using Bingol.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Bingol.Data
{
    public class BingolContext
    {
        private static readonly string serverName = "localhost";
        private static readonly string database = "bingol";
        private static readonly string connectionString = $"Data Source={serverName};Initial Catalog={database};Integrated Security=True";

        public DataTable GetArray(string query)
        {
            DataTable dataTable = new DataTable();
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlDataAdapter sqlData = new SqlDataAdapter(query, connection);
            sqlData.Fill(dataTable);
            connection.Close();
            return dataTable;
        }
        public void Execute(string query)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteReader();
            connection.Close();
        }
    }
}
