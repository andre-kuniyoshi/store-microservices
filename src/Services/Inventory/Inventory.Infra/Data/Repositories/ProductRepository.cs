using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces.Repositories;
using Inventory.Infra.Data.Context;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infra.Data.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
       
        public ProductRepository(IDbContext context) : base(context, "Products")
        {
            
        }
    }
}
