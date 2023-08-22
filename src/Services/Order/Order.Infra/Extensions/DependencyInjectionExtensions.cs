using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Contracts.Infrastructure;
using Order.Application.Contracts.Persistence;
using Order.Application.Models;
using Order.Infra.Data.Context;
using Order.Infra.Data.Repositories;
using Order.Infra.Integrations.Mail;

namespace Order.Infra.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInfraLayer(this IServiceCollection services, IConfiguration configuration)
        {
            // Option Pattern
            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));

            // EF Core
            services.AddDbContext<OrderContext>(options => options.UseSqlServer(configuration.GetConnectionString("OrderConnectionString")));

            // External Services
            services.AddScoped<IEmailService, EmailService>();

            // Repositories
            services.AddScoped(typeof(IAsyncRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
