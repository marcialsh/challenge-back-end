using System.Data;
using Microsoft.Data.Sqlite;

namespace BlogStarWars.Infrastructure.Data.Dapper.DapperConnection
{
    public class DapperConnectionFactory
    {
        public DapperConnectionFactory(string connectionString) => _connectionString = connectionString;
        private readonly string _connectionString;

        public IDbConnection CreateConnection() => new SqliteConnection(_connectionString);
    }
}