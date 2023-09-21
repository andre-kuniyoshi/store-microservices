using Register.Application.Interfaces;
using Register.Infra.Data.Context;
using Register.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using Register.Infra.BusConsumer;
using EventBus.Messages.Common;

namespace Register.Data.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInfraLayer(this IServiceCollection services, IConfiguration configuration)
        { 
            services.AddDbContext<RegistersDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("RegisterDB")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();

            services.AddMassTransit(config => {

                config.AddConsumer<RegisterNewUserConsumer>();

                config.UsingRabbitMq((ctx, cfg) => {
                    cfg.Host(configuration["EventBusSettings:HostAddress"]);

                    cfg.ReceiveEndpoint(EventBusConstants.RegisterNewUserQueue, c => {
                        c.ConfigureConsumer<RegisterNewUserConsumer>(ctx);
                    });
                });
            });

            return services;
        }
    }
}
