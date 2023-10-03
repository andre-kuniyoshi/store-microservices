using Basket.API.Data.Repositories;
using Basket.API.GrpcServicesClients;
using Basket.API.Services;
using MassTransit;
using Inventory.Grpc.Protos;

namespace Basket.API
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketService, BasketService>();

            services.AddGrpcClient<ProductProtoService.ProductProtoServiceClient>(opt => opt.Address = new Uri(configuration.GetValue<string>("GrpcSettings:InventoryUrl")!));

            services.AddScoped<ProductGrpcService>();

            services.AddMassTransit(config => 
            {
                config.UsingRabbitMq((ctx, cfg) => 
                {
                    cfg.Host(configuration["EventBusSettings:HostAddress"]);
                });
            });

            services.AddAutoMapper(typeof(Program));

            return services;
        }
    }
}