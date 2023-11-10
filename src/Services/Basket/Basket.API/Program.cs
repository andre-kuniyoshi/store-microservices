using Basket.Application.Extensions;
using Basket.Infra.Extensions;
using Core.Configurations;
using Core.DependencyInjectionExtension;
using Serilog;

namespace Basket.API
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
            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddInfraLayer(builder.Configuration);
            builder.Services.AddApplicationLayer();
            builder.Services.AddCoreLib();

            builder.Services.AddSwaggerConfigs(builder.Environment);

            var app = builder.Build();

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