using System.Data;

namespace Infrastructure.Connection
{
    public interface ISqlConnection
    {
        IDbConnection CreateConnection();
    }
}
