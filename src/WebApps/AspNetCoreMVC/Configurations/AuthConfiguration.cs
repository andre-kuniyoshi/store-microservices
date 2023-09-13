using Microsoft.AspNetCore.Authentication.Cookies;

namespace AspNetCoreMVC.Configurations
{
    public static class AuthConfiguration
    {
        public static IServiceCollection AddAuthConfigurations(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/login";
                options.LogoutPath = "/logout";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(50);
                options.SlidingExpiration = false;
            });

            return services;
        }
    }
}
