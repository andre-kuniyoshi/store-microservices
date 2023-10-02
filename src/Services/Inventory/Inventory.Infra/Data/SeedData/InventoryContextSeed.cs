using Dapper;
using Inventory.Infra.Data.Context;
using Inventory.Infra.Data.SQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Inventory.Infra.Data.SeedData
{
    public static class InventoryContextSeed
    {
        public static async Task MigrateDatabase(WebApplication webApp, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            using (var scope = webApp.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerContext = services.GetRequiredService<ILogger<PostgresContext>>();
                var context = services.GetRequiredService<IDbContext>();
                var nameOrderContext = typeof(PostgresContext);

                try
                {
                    loggerContext.LogInformation("Starting create database associated with context {DbContextName}", nameOrderContext);

                    await context!.Connection.ExecuteAsync(ProductScripts.CreateTableAndInsertValues);
                    await context!.Connection.ExecuteAsync(SaleScripts.CreateTableAndInsertValues);
                    await context!.Connection.ExecuteAsync(CouponScripts.CreateTableAndInsertValues);

                    loggerContext.LogInformation("Migrated database associated with context {DbContextName} and seeded data", nameOrderContext);
                }
                catch (Exception ex)
                {
                    loggerContext.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", nameOrderContext);

                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;

                        Thread.Sleep(2000);
                         await MigrateDatabase(webApp, retryForAvailability);
                    }
                }
            }
        }
    }
}
