using System.Data;

namespace Inventory.Infra.Data.ConnectionFactory
{
    public interface IDbConnectionFactory
    {
        public IDbConnection CrateConnection();
    }
}
