using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Infra.Data.Context;
using Ordering.Infra.Data.Repositories;
using Ordering.Infra.Integrations.Mail;

namespace Ordering.Infra.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInfraLayer(this IServiceCollection services, IConfiguration configuration)
        {
            // Option Pattern
            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));

            // EF Core
            services.AddDbContext<OrderContext>(options => options.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString")));

            // External Services
            services.AddScoped<IEmailService, EmailService>();

            // Repositories
            services.AddScoped(typeof(IAsyncRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
