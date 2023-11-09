using Basket.Domain.Interfaces.GrpcServiceClients;
using Basket.Domain.Interfaces.Repositories;
using Basket.Infra.Data.Repositories;
using Basket.Infra.Grpc.Protos;
using Basket.Infra.Grpc.ServiceClients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Infra.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddInfraLayer(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IBasketRepository, BasketRepository>();

            services.AddGrpcClient<ProductProtoService.ProductProtoServiceClient>(opt => opt.Address = new Uri(config.GetValue<string>("GrpcSettings:InventoryUrl")!));

            services.AddScoped<IInventoryGrpcServiceClient, InventoryGrpcService>();

            return services;
        }
    }
}
