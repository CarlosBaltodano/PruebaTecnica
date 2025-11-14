using System.Data;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Connection
{
    public class SqlConnection : ISqlConnection
    {
        private readonly string _cn;

        public SqlConnection(IConfiguration config)
        {
            _cn = config.GetConnectionString("PruebaTecnicaDB")!;
        }

        public IDbConnection CreateConnection() => new Microsoft.Data.SqlClient.SqlConnection(_cn);
    }
}
