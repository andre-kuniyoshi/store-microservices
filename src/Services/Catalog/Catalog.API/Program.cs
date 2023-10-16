using Catalog.API.Data.SeedData;
using Core.Configurations;
using Core.DependencyInjectionExtension;
using Serilog;

namespace Catalog.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.ConfigureAppSettings();
            builder.Host.AddSerilog();

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddDependencyInjection();
            builder.Services.AddCoreLib();

            builder.Services.AddSwaggerConfigs(builder.Environment);

            var app = builder.Build();

            app.Lifetime.ApplicationStarted.Register(async () => await CatalogContextSeed.SeedData(app.Configuration));

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