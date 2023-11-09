using Basket.API.Data.Repositories;
using Basket.API.GrpcServicesClients;
using Basket.API.Services;
using MassTransit;

namespace Basket.API
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {

            
            

            

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