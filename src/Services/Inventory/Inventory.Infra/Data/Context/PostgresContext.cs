using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Discount.Infra.Data.Context
{
    public sealed class PostgresContext : IDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public PostgresContext(IConfiguration _configuration)
        {
            _configuration = _configuration ?? throw new ArgumentNullException();
            _connectionString = _configuration.GetValue<string>("DatabaseSettings:ConnectionString");
        }

        public IDbConnection CrateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
