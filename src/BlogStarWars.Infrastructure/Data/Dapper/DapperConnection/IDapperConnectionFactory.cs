using System.Data;

namespace BlogStarWars.Infrastructure.Data.Dapper.DapperConnection
{
    public interface IDapperConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}