using AspNetCoreMVC.Data;
using OpenIddict.Client;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace AspNetCoreMVC.Configurations
{
    public static class OpeniddictConfigurations
    {
        public static IServiceCollection AddOpeniddictConfigurations(this IServiceCollection services)
        {
            services.AddOpenIddict()

             // Register the OpenIddict core components.
             .AddCore(options =>
             {
                 // Configure OpenIddict to use the Entity Framework Core stores and models.
                 // Note: call ReplaceDefaultEntities() to replace the default OpenIddict entities.
                 options.UseEntityFrameworkCore()
                        .UseDbContext<ApplicationDbContext>();

                 // Developers who prefer using MongoDB can remove the previous lines
                 // and configure OpenIddict to use the specified MongoDB database:
                 // options.UseMongoDb()
                 //        .UseDatabase(new MongoClient().GetDatabase("openiddict"));

                 // Enable Quartz.NET integration.
                 options.UseQuartz();
             })

             // Register the OpenIddict client components.
             .AddClient(options =>
             {
                 // Note: this sample uses the code flow, but you can enable the other flows if necessary.
                 options.AllowAuthorizationCodeFlow();

                 // Register the signing and encryption credentials used to protect
                 // sensitive data like the state tokens produced by OpenIddict.
                 options.AddDevelopmentEncryptionCertificate()
                        .AddDevelopmentSigningCertificate();

                 // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
                 options.UseAspNetCore()
                        .EnableStatusCodePagesIntegration()
                        .EnableRedirectionEndpointPassthrough()
                        .EnablePostLogoutRedirectionEndpointPassthrough();

                 // Register the System.Net.Http integration and use the identity of the current
                 // assembly as a more specific user agent, which can be useful when dealing with
                 // providers that use the user agent as a way to throttle requests (e.g Reddit).
                 options.UseSystemNetHttp()
                        .SetProductInformation(typeof(Program).Assembly);

                 // Add a client registration matching the client application definition in the server project.
                 options.AddRegistration(new OpenIddictClientRegistration
                 {
                     Issuer = new Uri("https://localhost:44313/", UriKind.Absolute),

                     ClientId = "mvc",
                     ClientSecret = "901564A5-E7FE-42CB-B10D-61EF6A8F3654",
                     Scopes = { Scopes.Email, Scopes.Profile },

                     // Note: to mitigate mix-up attacks, it's recommended to use a unique redirection endpoint
                     // URI per provider, unless all the registered providers support returning a special "iss"
                     // parameter containing their URL as part of authorization responses. For more information,
                     // see https://datatracker.ietf.org/doc/html/draft-ietf-oauth-security-topics#section-4.4.
                     RedirectUri = new Uri("callback/login/local", UriKind.Relative),
                     PostLogoutRedirectUri = new Uri("callback/logout/local", UriKind.Relative)
                 });
             });

            return services;
        }
    }
}
