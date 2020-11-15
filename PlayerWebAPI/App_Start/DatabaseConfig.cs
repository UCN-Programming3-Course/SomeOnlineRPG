using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PlayerWebAPI
{
    public static class DatabaseConfig
    {
        private static string _connectionString;

        public static void Register(string connectionString)
        {
            if (connectionString is null)
            {
                throw new ArgumentNullException(nameof(connectionString));
            }
            _connectionString = connectionString;
        }

        internal static IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}