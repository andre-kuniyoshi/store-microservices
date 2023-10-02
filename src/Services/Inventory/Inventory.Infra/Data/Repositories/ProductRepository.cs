using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces.Repositories;
using Inventory.Infra.Data.Context;

namespace Inventory.Infra.Data.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
       
        public ProductRepository(IDbContext context) : base(context, "Products")
        {
            
        }
    }
}
