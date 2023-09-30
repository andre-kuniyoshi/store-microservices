using Core.DependencyInjectionExtension;
using Discount.Application.Services;
using Discount.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Discount.Application.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {

            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddCoreLib();

            return services;
        }
    }
}
