using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Identity.Infra.Data.Context;
using Identity.Application.Domain;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Identity.Infra.Data.Seed
{
    public class AppIdentityDbContextSeed
    {
        public static async Task MigrateDatabase(WebApplication webApp, int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            var scopedFactory = webApp.Services.GetService<IServiceScopeFactory>();

            using (var scope = scopedFactory.CreateScope())
            {
                var services = scope.ServiceProvider;

                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var applicationManager = services.GetRequiredService<IOpenIddictApplicationManager>();

                var loggerContext = services.GetRequiredService<ILogger<AppIdentityDbContext>>();

                var context = services.GetService<AppIdentityDbContext>();
                var nameOrderContext = typeof(AppIdentityDbContext);

                try
                {
                    loggerContext.LogInformation("Starting migration database associated with context {DbContextName}", nameOrderContext);

                    context.Database.Migrate();

                    //await SeedRolesAsync(roleManager);

                    await SeedUsersAsync(userManager);
                    await SeedApplicationAsync(applicationManager);

                    //await SeedUserClaimsAsync(userManager);

                    loggerContext.LogInformation("Migrated database associated with context {DbContextName} and seeded data", nameOrderContext);
                }
                catch (SqlException ex)
                {
                    loggerContext.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", nameOrderContext);

                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;

                        Thread.Sleep(2000);
                        MigrateDatabase(webApp, retryForAvailability);
                    }
                }
                catch (Exception ex)
                {
                    loggerContext.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", nameOrderContext);
                }
            }
        }

        private static async Task SeedApplicationAsync(IOpenIddictApplicationManager applicationManager)
        {
            if (await applicationManager.FindByClientIdAsync("c05471b2-c723-4232-8c1a-244c1fc2a4af") is null)
            {
                await applicationManager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "c05471b2-c723-4232-8c1a-244c1fc2a4af",
                    ClientSecret = "aspnetmvc-secret",
                    ConsentType = ConsentTypes.Explicit,
                    DisplayName = "AspNetMVC",
                    RedirectUris =
                    {
                        new Uri("https://localhost:5021/callback/login/local")
                    },
                    PostLogoutRedirectUris =
                    {
                        new Uri("https://localhost:5021/callback/logout/local")
                    },
                    Permissions =
                    {
                        Permissions.Endpoints.Authorization,
                        Permissions.Endpoints.Logout,
                        Permissions.Endpoints.Token,
                        Permissions.GrantTypes.AuthorizationCode,
                        Permissions.ResponseTypes.Code,
                        Permissions.Scopes.Email,
                        Permissions.Scopes.Profile,
                        Permissions.Scopes.Roles
                    },
                    Requirements =
                    {
                        Requirements.Features.ProofKeyForCodeExchange
                    }
                });
            }

            if (await applicationManager.FindByClientIdAsync("e9c2796d-cf5c-4a9e-96d9-4b8e73549b09") is null)
            {
                await applicationManager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "e9c2796d-cf5c-4a9e-96d9-4b8e73549b09",
                    ClientSecret = "postman-secret",
                    DisplayName = "Postman",
                    RedirectUris =
                    {
                        new Uri("https://oauth.pstmn.io/v1/callback")
                    },
                    Permissions =
                    {
                        Permissions.Endpoints.Authorization,
                        Permissions.Endpoints.Logout,
                        Permissions.Endpoints.Token,
                        Permissions.Endpoints.Introspection,

                        Permissions.GrantTypes.AuthorizationCode,
                        Permissions.GrantTypes.RefreshToken,
                        Permissions.ResponseTypes.Code,

                        Permissions.Scopes.Email,
                        Permissions.Scopes.Profile,
                        Permissions.Scopes.Roles,
                        Permissions.Prefixes.Scope + "api"
                    },
                    Requirements =
                    {
                        Requirements.Features.ProofKeyForCodeExchange
                    }
                });
            }
        }

        private static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (await userManager.FindByEmailAsync("user@teste.com") == null)
            {
                AppUser user = new AppUser();
                user.UserName = "user@teste.com";
                user.Email = "user@teste.com";
                user.NormalizedUserName = "USER@TESTE.COM";
                user.NormalizedEmail = "USER@TESTE.COM";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = await userManager.CreateAsync(user, "Teste#2023");

                if (result.Succeeded)
                {
                    //await userManager.AddToRoleAsync(user, "User");
                }
            }

            if (await userManager.FindByEmailAsync("admin@teste.com") == null)
            {
                AppUser user = new AppUser();
                user.UserName = "admin@teste.com";
                user.Email = "admin@teste.com";
                user.NormalizedUserName = "ADMIN@TESTE.COM";
                user.NormalizedEmail = "ADMIN@TESTE.COM";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = await userManager.CreateAsync(user, "Teste#2023");

                if (result.Succeeded)
                {
                    //await userManager.AddToRoleAsync(user, "Admin");
                }
            }

            if (await userManager.FindByEmailAsync("manager@teste.com") == null)
            {
                AppUser user = new AppUser();
                user.UserName = "manager@teste.com";
                user.Email = "manager@teste.com";
                user.NormalizedUserName = "MANAGER@TESTE.COM";
                user.NormalizedEmail = "MANAGER@TESTE.COM";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = await userManager.CreateAsync(user, "Teste#2023");

                if (result.Succeeded)
                {
                    //await userManager.AddToRoleAsync(user, "Manager");
                }
            }
        }
    }
}
