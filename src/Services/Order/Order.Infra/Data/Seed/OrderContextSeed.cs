using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Order.Domain.Entities;
using Order.Infra.Data.Context;

namespace Order.Infra.Data.Seed
{
    public class OrderContextSeed
    {
        public static async Task MigrateDatabase(WebApplication webApp, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            using (var scope = webApp.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerContext = services.GetRequiredService<ILogger<OrderContext>>();
                var loggerSeeder = services.GetRequiredService<ILogger<OrderContextSeed>>();
                var context = services.GetService<OrderContext>();
                var nameOrderContext = typeof(OrderContext);

                try
                {
                    loggerContext.LogInformation("Starting migration database associated with context {DbContextName}", nameOrderContext);

                    context.Database.Migrate();
                    await SeedAsync(context, loggerSeeder);

                    loggerContext.LogInformation("Migrated database associated with context {DbContextName} and seeded data", nameOrderContext);
                }
                catch (SqlException ex)
                {
                    loggerContext.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", nameOrderContext);

                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;

                        Thread.Sleep(2000);
                        MigrateDatabase(webApp, retryForAvailability);
                    }
                }
                catch (Exception ex)
                {
                    loggerContext.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", nameOrderContext);

                }
            }
        }

        private static async Task SeedAsync(OrderContext context, ILogger<OrderContextSeed> logger)
        {
            if (!context.Orders.Any())
            {
                context.Orders.AddRange(GetPreconfiguredOrders());
                await context.SaveChangesAsync();
                logger.LogInformation("Seed Orders table");
            }
        }

        private static IEnumerable<PurchaseOrder> GetPreconfiguredOrders()
        {
            return new List<PurchaseOrder>
            {
               
            };
        }
    }
}
