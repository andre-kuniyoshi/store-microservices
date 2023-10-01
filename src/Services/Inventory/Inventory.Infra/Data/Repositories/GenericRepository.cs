using Inventory.Domain.Interfaces.Repositories;
using Inventory.Infra.Data.Context;

namespace Inventory.Infra.Data.Repositories
{
    public class GenericRepository : IGenericRepository
    {
        private readonly IDbContext _dbContext;

        public GenericRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
