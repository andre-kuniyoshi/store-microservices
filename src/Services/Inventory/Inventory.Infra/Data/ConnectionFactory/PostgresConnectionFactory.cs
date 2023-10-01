using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Inventory.Infra.Data.ConnectionFactory
{
    public sealed class PostgresConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public PostgresConnectionFactory(IConfiguration _configuration)
        {
            _configuration = _configuration ?? throw new ArgumentNullException();
            _connectionString = _configuration.GetValue<string>("DatabaseSettings:ConnectionString") ?? throw new ArgumentNullException("Check db connection string in appsetings");
        }

        public IDbConnection CrateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }

    }
}
