using Register.Application.Interfaces;
using Register.Application.NotificationPattern;
using Register.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Register.Application.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INotifier, Notifier>();

            return services;
        }
    }
}
