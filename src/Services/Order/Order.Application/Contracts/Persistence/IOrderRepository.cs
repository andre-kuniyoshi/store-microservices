using Order.Domain.Entities;

namespace Order.Application.Contracts.Persistence
{
    public interface IOrderRepository : IAsyncRepository<PurchaseOrder>
    {
        Task<IEnumerable<PurchaseOrder>> GetOrdersByClientId(Guid ClientId);
    }
}
