using Register.Application.Interfaces;
using Register.Infra.Data.Context;
using Register.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Register.Data.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInfraLayer(this IServiceCollection services, IConfiguration configuration)
        { 
            services.AddDbContext<RegistersDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("sqlServer")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();

            return services;
        }
    }
}
