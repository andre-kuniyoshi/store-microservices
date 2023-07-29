using Discount.Grpc.Protos;

namespace Basket.API.GrpcServicesClients
{
    public class DiscountGrpcServiceClient
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _grpcClient;

        public DiscountGrpcServiceClient(DiscountProtoService.DiscountProtoServiceClient grpcClient)
        {
            _grpcClient = grpcClient;
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            var discountRequest = new GetDiscountRequest { ProductName = productName };

            return await _grpcClient.GetDiscountAsync(discountRequest);
        }
    }
}
