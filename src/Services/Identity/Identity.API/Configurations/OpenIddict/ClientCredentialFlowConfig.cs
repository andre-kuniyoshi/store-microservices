using Identity.Infra.Data.Context;

namespace Identity.API.Configurations.OpenIddict
{
    public static class ClientCredentialFlowConfig
    {
        public static IServiceCollection AddClientCredentialFlowConfig(this IServiceCollection services)
        {
            services.AddOpenIddict()

                // Register the OpenIddict core components.
                .AddCore(options =>
                {
                    // Configure OpenIddict to use the EF Core stores/models.
                    options.UseEntityFrameworkCore()
                        .UseDbContext<AppIdentityDbContext>();
                })

                // Register the OpenIddict server components.
                .AddServer(options =>
                {
                    options
                        .AllowClientCredentialsFlow();

                    options
                        .SetTokenEndpointUris("/connect/token");

                    // Encryption and signing of tokens
                    options
                        .AddEphemeralEncryptionKey()
                        .AddEphemeralSigningKey()
                        .DisableAccessTokenEncryption();

                    // Register scopes (permissions)
                    options.RegisterScopes("api");

                    // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
                    options
                        .UseAspNetCore().EnableTokenEndpointPassthrough();
                });

            return services;
        }
    }
}
