using Order.Application.Extensions;
using Order.Infra.Extensions;
using Serilog;
using Core.Configurations;
using Order.Infra.Data.Seed;
using Order.API.Configurations;

namespace Order.API
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
            builder.Services.AddInfraLayer(builder.Configuration);

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddRabbitMQConfigs(builder.Configuration);

            builder.Services.AddSwaggerConfigs(builder.Environment);

            var app = builder.Build();

            app.Lifetime.ApplicationStarted.Register(() => OrderContextSeed.MigrateDatabase(app));

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.UseTokenParser();

            app.MapControllers();

            Log.Information($"Starting {app.Environment.ApplicationName} - {app.Environment.EnvironmentName}.");

            app.Run();
        }
    }
}