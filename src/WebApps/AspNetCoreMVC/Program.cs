using AspNetCoreMVC.Configurations;
using AspNetCoreMVC.Data;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Core.Configurations;
using Serilog;
using AspNetCoreMVC.Extensions;
using AspNetCoreMVC.Services;
using AspNetCoreMVC.Services.Interfaces;

namespace AspNetCoreMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.ConfigureAppSettings();
            builder.Host.AddSerilog();

            builder.Services.AddHttpClient<ICatalogService, CatalogService>(c =>
                c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiSettings:GatewayAddress")!));

            builder.Services.AddHttpClient<IBasketService, BasketService>(c =>
                c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiSettings:GatewayAddress")!));

            builder.Services.AddStoreServices();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthConfigurations();

            //OpenIddict offers native integration with Quartz.NET to perform scheduled tasks
            //(like pruning orphaned authorizations from the database) at regular intervals.
            builder.Services.AddQuartz(options =>
            {
               options.UseMicrosoftDependencyInjectionJobFactory();
               options.UseSimpleTypeLoader();
               options.UseInMemoryStore();
            });

            //Register the Quartz.NET service and configure it to block shutdown until jobs are complete.
            builder.Services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                // Configure the context to use sqlite.
                options.UseSqlServer(builder.Configuration.GetConnectionString("OpenIdMvcDB"));

                // Register the entity sets needed by OpenIddict.
                // Note: use the generic overload if you need
                // to replace the default OpenIddict entities.
                options.UseOpenIddict();
            });

            builder.Services.AddOpeniddictConfigurations(builder.Configuration);

            builder.Services.AddHttpClient();

            var app = builder.Build();

            app.Lifetime.ApplicationStarted.Register(() => SeedDatabase.MigrateDatabase(app));

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

            Log.Information($"Starting {app.Environment.ApplicationName} - {app.Environment.EnvironmentName}.");
            app.Run();
        }
    }
}