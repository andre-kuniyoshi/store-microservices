using Basket.Domain.Entities;
using Basket.Domain.Interfaces.GrpcServiceClients;
using Basket.Infra.Grpc.Protos;

namespace Basket.Infra.Grpc.ServiceClients
{
    public class InventoryGrpcService : IInventoryGrpcServiceClient
    {
        private readonly ProductProtoService.ProductProtoServiceClient _grpcClient;

        public InventoryGrpcService(ProductProtoService.ProductProtoServiceClient grpcClient)
        {
            _grpcClient = grpcClient;
        }

        public async Task<Product> GetProductPrice(Guid productId)
        {
            var productRequest = new GetProductPriceRequest { Id = productId.ToString() };

            var productModel = await _grpcClient.GetProductPriceAsync(productRequest);

            return new Product
            {
                Id = Guid.Parse(productModel.Id),
                Price = productModel.Price,
                Quantity = productModel.Quantity,
                Active = productModel.Active,
                CreatedBy = productModel.CreatedBy,
                CreatedDate = productModel.CreatedDate.ToDateTime(),
                LastModifiedBy = productModel.LastModifiedBy,
                LastModifiedDate = productModel.LastModifiedDate.ToDateTime(),

                Sale = new Sale
                {
                    Id = Guid.Parse(productModel.Sale.Id),
                    Price = productModel.Sale.Price,
                    StartDate = productModel.Sale.StartDate.ToDateTime(),
                    EndDate = productModel.Sale.EndDate.ToDateTime(),
                    Active = productModel.Sale.Active,
                    CreatedBy = productModel.Sale.CreatedBy,
                    CreatedDate = productModel.Sale.CreatedDate.ToDateTime(),
                    LastModifiedBy = productModel.Sale.LastModifiedBy,
                    LastModifiedDate = productModel.Sale.LastModifiedDate.ToDateTime()
                }
            };
        }
    }
}
