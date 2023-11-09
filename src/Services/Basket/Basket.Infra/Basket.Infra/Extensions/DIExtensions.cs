using Basket.Domain.Interfaces.Repositories;
using Basket.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Infra.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddInfraLayer(this IServiceCollection services)
        {
            services.AddScoped<IBasketRepository, BasketRepository>();
            return services;
        }
    }
}
