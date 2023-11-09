
namespace Basket.Domain.Interfaces.GrpcServiceClients
{
    public interface IProductGrpcServiceClient
    {
        public Task<ProductModel> GetProductPrice(Guid productId);
    }
}
