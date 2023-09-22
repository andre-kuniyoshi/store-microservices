using Core.Configurations;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;

namespace OcelotApiGateway
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.ConfigureAppSettings();

            builder.Host.AddSerilog();
            builder.Configuration
                .AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);


            builder.Services.AddRSAPublicKey(builder.Configuration);
            builder.Services.AddSecurityConfigs(builder.Configuration);

            builder.Services.AddOcelot();

            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapGet("/", () => "Hello World!");
                app.UseRouting();
                app.UseEndpoints(endpoints => endpoints.MapControllers());
            }
            app.UseHttpLoggingSerilog();
            await app.UseOcelot();
            app.UseAuthentication();
            app.UseAuthorization();

            Log.Information($"Starting {app.Environment.ApplicationName} - {app.Environment.EnvironmentName}.");
            app.Run();
        }
    }
}