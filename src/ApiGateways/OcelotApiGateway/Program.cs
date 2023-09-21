using Core.Configurations;
using Microsoft.IdentityModel.Tokens;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;
using OpenIddict.Validation.AspNetCore;
using System.Security.Cryptography;

namespace OcelotApiGateway
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.ConfigureAppSettings();

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

            await app.UseOcelot();
            app.UseAuthentication();
            app.UseAuthorization();


            app.Run();
        }
    }
}