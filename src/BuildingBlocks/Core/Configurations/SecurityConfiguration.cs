using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Validation.AspNetCore;

namespace Core.Configurations
{
    public static class SecurityConfiguration
    {
        public static IServiceCollection AddSecurityConfigs(this IServiceCollection services, IConfiguration config)
        {
            var issuer = config.GetValue<string>("OpeniddictConfigs:Issuer");

            if(String.IsNullOrEmpty(issuer)) throw new ArgumentNullException("Issuer must be set in appsettings");

            services.AddOpenIddict()
                .AddValidation(options =>
                {

                    // Register the ASP.NET Core host.
                    options.UseAspNetCore();

                    options.UseSystemNetHttp();

                    options.SetIssuer(issuer);

                });

            services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
            services.AddAuthorization();

            return services;
        }

    }
}
