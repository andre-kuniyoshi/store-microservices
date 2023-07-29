using Discount.Infra.Data.Context;
using Npgsql;

namespace Discount.API.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplication MigrateDatabase<TContext>(this WebApplication webApp, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            using (var scope = webApp.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();

                try
                {
                    logger.LogInformation("Migration postgresql database.");
                    var connectionFactory = services.GetService<IDbContext>();

                    using (var connection = connectionFactory.CrateConnection())
                    {
                        connection.Open();

                        using (var command = new NpgsqlCommand { Connection = (NpgsqlConnection)connection })
                        {
                            command.CommandText = "DROP TABLE IF EXISTS Coupon";
                            command.ExecuteNonQuery();

                            command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, 
                                                                ProductName VARCHAR(24) NOT NULL,
                                                                Description TEXT,
                                                                Amount INT)";
                            command.ExecuteNonQuery();


                            command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('IPhone X', 'IPhone Discount', 150);";
                            command.ExecuteNonQuery();

                            command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Samsung 10', 'Samsung Discount', 100);";
                            command.ExecuteNonQuery();

                            logger.LogInformation("Migrated postresql database.");
                        }
                    }

                }
                catch (NpgsqlException ex)
                {

                    logger.LogError(ex, "An error occurred while migrating the postresql database");

                    if(retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        Thread.Sleep(2000);
                        MigrateDatabase<TContext>(webApp, retryForAvailability);
                    }
                }
            }

            return webApp;
        }
    }
}
