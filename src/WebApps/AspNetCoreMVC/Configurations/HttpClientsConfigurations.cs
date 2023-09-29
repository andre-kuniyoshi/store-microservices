using AspNetCoreMVC.Services;
using AspNetCoreMVC.Services.Interfaces;
using Core.Middlewares;

namespace AspNetCoreMVC.Configurations
{
    public static class HttpClientsConfigurations
    {
        public static IServiceCollection AddStoreHttpClients(this IServiceCollection services, IConfiguration config)
        {
            services.AddHttpContextAccessor();
            services.AddHttpClient();
            services.AddHttpClient<ICatalogService, CatalogService>(c =>
                c.BaseAddress = new Uri(config.GetValue<string>("ApiSettings:GatewayAddress")!)
            );
            services.AddTransient<AuthenticationDelegatingHandler>();
            services.AddHttpClient<IBasketService, BasketService>(c =>
                c.BaseAddress = new Uri(config.GetValue<string>("ApiSettings:GatewayAddress")!)
            )
            .AddHttpMessageHandler<AuthenticationDelegatingHandler>();

            return services;
        }
    }
}
