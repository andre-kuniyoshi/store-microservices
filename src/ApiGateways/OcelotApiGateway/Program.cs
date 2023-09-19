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

            //
            var issuer = builder.Configuration.GetValue<string>("OpeniddictConfigs:Issuer");

            //var publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
            //var privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());

            //var key = new RsaSecurityKey(RSA.Create(2048))
            //{
            //    KeyId = Guid.NewGuid().ToString()
            //};

            if (String.IsNullOrEmpty(issuer)) throw new ArgumentNullException("Issuer must be set in appsettings");
            builder.Services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme).AddJwtBearer();

            builder.Services.AddOpenIddict()
                .AddValidation(options =>
                {

                    // Register the ASP.NET Core host.
                    options.SetIssuer(issuer);
                    options.UseSystemNetHttp();
                    options.UseAspNetCore();


                });

            builder.Services.AddOcelot().AddCacheManager(settings => settings.WithDictionaryHandle());
            //
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

            app.Run();
        }
    }
}