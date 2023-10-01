using System.Data;

namespace Inventory.Infra.Data.Context
{
    public interface IDbContext : IDisposable
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }
    }
}
