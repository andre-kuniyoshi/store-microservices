using Microsoft.EntityFrameworkCore;
using Order.Application.Contracts.Persistence;
using Order.Domain.Entities;
using Order.Infra.Data.Context;

namespace Order.Infra.Data.Repositories
{
    public class OrderRepository : GenericRepository<PurchaseOrder>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext) : base(dbContext)
        {        
        }

        public async Task<IEnumerable<PurchaseOrder>> GetOrdersByUserName(string userName)
        {
            var orderList = await _dbContext.Orders
                                .Where(o => o.UserName == userName)
                                .ToListAsync();
            return orderList;
        }
    }
}
