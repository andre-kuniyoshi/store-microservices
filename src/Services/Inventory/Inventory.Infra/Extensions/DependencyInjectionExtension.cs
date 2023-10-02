using Inventory.Domain.Interfaces.Repositories;
using Inventory.Infra.Data.Repositories;
using Inventory.Infra.Data.Context;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace Inventory.Infra.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddInfraLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbContext, PostgresContext>();

            services.AddScoped<ICouponRepository, CouponRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();

            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(configuration.GetValue<string>("EventBusSettings:HostAddress"));
                });
            });

            return services;
        }
    }
}
