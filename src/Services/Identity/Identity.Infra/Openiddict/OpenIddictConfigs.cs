using Identity.Infra.Data.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using static OpenIddict.Abstractions.OpenIddictConstants;
using Core.Configurations;

namespace Identity.Infra.Openiddict
{
    public static class OpenIdDictConfigs
    {
        public static IServiceCollection AddOpenIddictConfigs(this IServiceCollection services, IConfiguration config)
        {
            services.AddOpenIddict()
                // Register the OpenIddict core components.
                .AddCore(options =>
                {
                    // Configure OpenIddict to use the EF Core stores/models.
                    options.UseEntityFrameworkCore()
                        .UseDbContext<AppIdentityDbContext>();

                    options.UseQuartz();
                })

                // Register the OpenIddict server components.
                .AddServer(options =>
                {

                    options
                        .AllowAuthorizationCodeFlow()
                        .RequireProofKeyForCodeExchange();

                    options
                        .SetTokenEndpointUris("/connect/token")
                        .SetAuthorizationEndpointUris("/connect/authorize")
                        .SetLogoutEndpointUris("connect/logout")
                        .SetIntrospectionEndpointUris("/connect/token/introspect")
                        .SetUserinfoEndpointUris("/connect/userinfo");

                    RsaSecurityKey rsa = services.BuildServiceProvider().GetRequiredService<RsaSecurityKey>();
                    // Encryption and signing of tokens
                    options
                        .AddEphemeralEncryptionKey()
                        //.AddEphemeralSigningKey()
                        .DisableAccessTokenEncryption()
                        .AddSigningKey(rsa);

                    // Register scopes (permissions)
                    options.RegisterScopes(Scopes.Email, Scopes.Profile, Scopes.Roles, "api");

                    // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
                    options
                        .UseAspNetCore()
                        .DisableTransportSecurityRequirement()
                        .EnableTokenEndpointPassthrough()
                        .EnableLogoutEndpointPassthrough()
                        .EnableAuthorizationEndpointPassthrough()
                        .EnableUserinfoEndpointPassthrough();
                })

                // Register the OpenIddict validation components.
                .AddValidation(options =>
                {
                    // Import the configuration from the local OpenIddict server instance.
                    options.UseLocalServer();

                    // Register the ASP.NET Core host.
                    options.UseAspNetCore();
                });

            return services;
        }
    }
}
