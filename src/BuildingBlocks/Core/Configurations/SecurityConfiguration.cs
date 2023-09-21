using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Validation.AspNetCore;
using System.Security.Cryptography;

namespace Core.Configurations
{
    public static class SecurityConfiguration
    {
        public static IServiceCollection AddRSAPublicKey(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<RsaSecurityKey>(provider =>
            {
                
                RSA rsa = RSA.Create();

                rsa.ImportFromPem(File.ReadAllText(config.GetValue<string>("OpeniddictConfigs:RSAPublicKeyPath")));

                return new RsaSecurityKey(rsa);
            });

            return services;
        }

        public static IServiceCollection AddRSAKeys(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<RsaSecurityKey>(provider =>
            {
                var publicKeyPath = config.GetValue<string>("OpeniddictConfigs:RSAPublicKeyPath");
                var privateKeyPath= config.GetValue<string>("OpeniddictConfigs:RSAPrivateKeyPath");

                RSA rsa = RSA.Create();
                rsa.ImportFromPem(File.ReadAllText(publicKeyPath));
                rsa.ImportFromPem(File.ReadAllText(privateKeyPath));

                return new RsaSecurityKey(rsa);
            });

            return services;
        }

        public static IServiceCollection AddSecurityOpeniddcitConfigs(this IServiceCollection services, IConfiguration config)
        {
            
            var issuer = config.GetValue<string>("OpeniddictConfigs:Issuer");

            if(String.IsNullOrEmpty(issuer)) throw new ArgumentNullException("Issuer must be set in appsettings");

            services.AddOpenIddict()
                .AddValidation(options =>
                {

                    // Register the ASP.NET Core host.
                    options.SetIssuer(new Uri(issuer));
                    options.UseSystemNetHttp();
                    options.UseAspNetCore();


                });

            services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
            services.AddAuthorization();

            return services;
        }

        public static IServiceCollection AddSecurityConfigs(this IServiceCollection services, IConfiguration config)
        {
            var issuer = config.GetValue<string>("OpeniddictConfigs:Issuer");
            RsaSecurityKey rsa = services.BuildServiceProvider().GetRequiredService<RsaSecurityKey>();

            services.AddAuthentication()
            .AddJwtBearer(options =>
            {

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                options.IncludeErrorDetails = true; // <- great for debugging

                // Configure the actual Bearer validation
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = rsa,
                    //ValidAudience = "jwt-test",
                    ValidIssuer = issuer,
                    
                    RequireSignedTokens = true,
                    RequireExpirationTime = true, // <- JWTs are required to have "exp" property set
                    ValidateLifetime = true, // <- the "exp" will be validated
                    ValidateAudience = false,
                    ValidateIssuer = true,
                };

                
            });

            return services;

        }

    }
}
