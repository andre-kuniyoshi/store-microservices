using Core.DependencyInjectionExtension;
using Inventory.Application.Services;
using Inventory.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Application.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {

            services.AddScoped<ICouponService, CouponService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddCoreLib();

            return services;
        }
    }
}
