using Inventory.Grpc.Protos;

namespace Basket.Infra.Grpc.ServiceClients
{
    public class ProductGrpcService
    {
        private readonly ProductProtoService.ProductProtoServiceClient _grpcClient;

        public ProductGrpcService(ProductProtoService.ProductProtoServiceClient grpcClient)
        {
            _grpcClient = grpcClient;
        }

        public async Task<ProductModel> GetProductPrice(Guid productId)
        {
            var productRequest = new GetProductPriceRequest { Id = productId.ToString() };

            return await _grpcClient.GetProductPriceAsync(productRequest);
        }
    }
}
