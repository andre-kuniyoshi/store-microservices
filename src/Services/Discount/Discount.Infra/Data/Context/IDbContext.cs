using System.Data;

namespace Discount.Infra.Data.Context
{
    public interface IDbContext
    {
        public IDbConnection CrateConnection();
    }
}
