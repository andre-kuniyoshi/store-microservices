
using Basket.Domain.Entities;

namespace Basket.Domain.Interfaces.GrpcServiceClients
{
    public interface IInventoryGrpcServiceClient
    {
        public Task<Product> GetProductPrice(Guid productId);
    }
}
