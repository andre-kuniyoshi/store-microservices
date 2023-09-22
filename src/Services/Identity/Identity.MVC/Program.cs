using Identity.Infra.Data.Seed;
using Identity.Infra.Extensions;
using Core.Configurations;
using Serilog;

namespace Identity.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = WebApplication.CreateBuilder(args);

            builder.ConfigureAppSettings();

            builder.Host.AddSerilog();

            builder.Services.AddInfraLayer(builder.Configuration);
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();
            app.Lifetime.ApplicationStarted.Register(() => AppIdentityDbContextSeed.MigrateDatabase(app));

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            Log.Information($"Starting {app.Environment.ApplicationName} - {app.Environment.EnvironmentName}.");
            app.Run();
        }
    }
}