using AspNetCoreMVC.Services;
using AspNetCoreMVC.Services.Interfaces;

namespace AspNetCoreMVC.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddStoreServices(this IServiceCollection services)
        {
            //services.AddScoped<ICatalogService, CatalogService>();

            return services;
        }
    }
}
