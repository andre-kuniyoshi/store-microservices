using Discount.Domain.Interfaces.Repositories;
using Discount.Infra.Data.Context;
using Discount.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Discount.Infra.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddInfraLayer(this IServiceCollection services)
        {
            services.AddScoped<IDbContext, PostgresContext>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();

            return services;
        }
    }
}
