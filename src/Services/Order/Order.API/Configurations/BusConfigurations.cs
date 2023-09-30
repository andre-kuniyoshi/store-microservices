using EventBus.Messages.Common;
using MassTransit;
using Order.API.EventBusConsumer;

namespace Order.API.Configurations
{
    public static class BusConfigurations
    {
        public static IServiceCollection AddRabbitMQConfigs(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<BasketCheckoutConsumer>();

            services.AddMassTransit(options => {

                options.AddConsumer<BasketCheckoutConsumer>();

                options.UsingRabbitMq((ctx, cfg) => {
                    cfg.Host(configuration.GetValue<string>("EventBusSettings:HostAddress"));

                    cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c => {
                        c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
                    });
                });
            });

            return services;
        }
    }
}
