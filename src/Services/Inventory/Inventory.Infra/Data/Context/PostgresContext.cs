using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Inventory.Infra.Data.Context
{
    public class PostgresContext : IDbContext
    {
        public IDbConnection Connection { get; }

        public IDbTransaction Transaction { get; set; }

        public PostgresContext(IConfiguration configuration)
        {
            Connection = new NpgsqlConnection(configuration.GetConnectionString("InventoryDB") ?? throw new ArgumentNullException("Check db connection string in appsettings"));
            Connection.Open();
        }

        public void Dispose()
        {
            Connection?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
