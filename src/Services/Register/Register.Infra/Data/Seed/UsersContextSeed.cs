using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Register.Application.Domain.Entities;
using Register.Infra.Data.Context;

namespace Order.Infra.Data.Seed
{
    public class RegistersContextSeed
    {
        public static async Task MigrateDatabase(WebApplication webApp, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            using (var scope = webApp.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerContext = services.GetRequiredService<ILogger<RegistersDbContext>>();
                var loggerSeeder = services.GetRequiredService<ILogger<RegistersContextSeed>>();
                var context = services.GetService<RegistersDbContext>();
                var nameRegistersContext = typeof(RegistersDbContext);

                try
                {
                    loggerContext.LogInformation("Starting migration database associated with context {DbContextName}", nameRegistersContext);

                    context.Database.Migrate();
                    await SeedAsync(context, loggerSeeder);

                    loggerContext.LogInformation("Migrated database associated with context {DbContextName} and seeded data", nameRegistersContext);
                }
                catch (SqlException ex)
                {
                    loggerContext.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", nameRegistersContext);

                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;

                        Thread.Sleep(2000);
                        MigrateDatabase(webApp, retryForAvailability);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private static async Task SeedAsync(RegistersDbContext context, ILogger<RegistersContextSeed> logger)
        {
            if (!context.Users.Any() && !context.Addresses.Any())
            {
                context.Users.AddRange(GetPreconfiguredRegisters());
                await context.SaveChangesAsync();
                logger.LogInformation("Seed Register table");
            }
        }

        private static IEnumerable<User> GetPreconfiguredRegisters()
        {
            Console.WriteLine("./Data/Seed/Register.json");
            var RegistersData = File.ReadAllText("../Register.Infra/Data/Seed/Register.json");
            var Register = JsonSerializer.Deserialize<IEnumerable<User>>(RegistersData);

            foreach (var item in Register)
            {
                item.Active = true;
                item.CreatedBy = "Admin";
                item.CreatedDate = DateTime.Now;
            }

            return Register;
        }
    }
}
