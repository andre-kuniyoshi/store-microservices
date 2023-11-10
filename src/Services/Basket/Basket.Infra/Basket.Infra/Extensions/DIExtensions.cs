using Basket.Domain.Interfaces.GrpcServiceClients;
using Basket.Domain.Interfaces.Repositories;
using Basket.Infra.Data.Repositories;
using Basket.Infra.Grpc.Protos;
using Basket.Infra.Grpc.ServiceClients;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Infra.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddInfraLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBasketRepository, BasketRepository>();

            services.AddGrpcClient<ProductProtoService.ProductProtoServiceClient>(opt => opt.Address = new Uri(configuration.GetValue<string>("GrpcSettings:InventoryUrl")!));

            services.AddScoped<IInventoryGrpcServiceClient, InventoryGrpcService>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
            });

            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(configuration["EventBusSettings:HostAddress"]);
                });
            });

            return services;
        }
    }
}
