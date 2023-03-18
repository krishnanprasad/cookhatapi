using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace cookhatAPI.Connection
{
    [ExcludeFromCodeCoverage]
    public class SqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;
        private SqlConnection _connection;

        public SqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection CreateConnection()
        {
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
            return _connection;
        }

        public SqlConnection CreateConnection(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            return _connection;
        }

        public void Dispose()
        {
            if (_connection != null)
                _connection.Dispose();
        }
    }
}
