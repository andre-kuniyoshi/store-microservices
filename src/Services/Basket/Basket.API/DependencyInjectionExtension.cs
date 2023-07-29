using Basket.API.Data.Repositories;
using Basket.API.GrpcServicesClients;
using Basket.API.Services;
using Discount.Grpc.Protos;
using MassTransit;

namespace Basket.API
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketService, BasketService>();

            services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(opt => opt.Address = new Uri(configuration["GrpcSettings:DiscountUrl"]));

            services.AddScoped<DiscountGrpcServiceClient>();

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