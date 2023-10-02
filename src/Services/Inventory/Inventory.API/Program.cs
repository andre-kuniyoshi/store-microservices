using Inventory.Infra.Extensions;
using Inventory.Application.Extensions;
using Core.Configurations;
using Serilog;
using Inventory.Infra.Data.SeedData;

namespace Inventory.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.ConfigureAppSettings();
            builder.Host.AddSerilog();

            builder.Services.AddControllers();

            builder.Services.AddApplicationLayer();
            builder.Services.AddInfraLayer();

            builder.Services.AddSwaggerConfigs(builder.Environment);

            var app = builder.Build();

            app.Lifetime.ApplicationStarted.Register(async () => await InventoryContextSeed.MigrateDatabase(app));

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            Log.Information($"Starting {app.Environment.ApplicationName} - {app.Environment.EnvironmentName}.");

            app.Run();
        }
    }
}