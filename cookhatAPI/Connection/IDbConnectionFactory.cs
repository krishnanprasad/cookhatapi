using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace cookhatAPI.Connection
{
    public interface IDbConnectionFactory : IDisposable
    {
        SqlConnection CreateConnection();
        SqlConnection CreateConnection(string connectionString);
    }
}
