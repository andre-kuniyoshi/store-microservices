using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace OcelotApiGateway
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

            builder.Services.AddOcelot().AddCacheManager(settings => settings.WithDictionaryHandle());

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