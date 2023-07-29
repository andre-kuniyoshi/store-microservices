using Core.NotifierErrors;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyInjectionExtension
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddCoreLib(this IServiceCollection services)
        {

            services.AddScoped<INotifier, Notifier>();
            return services;
        }
    }
}