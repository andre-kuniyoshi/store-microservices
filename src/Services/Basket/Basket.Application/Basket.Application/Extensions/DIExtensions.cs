using Basket.Application.Services;
using Basket.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Application.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<IBasketService, BasketService>();
            return services;
        }
    }
}
