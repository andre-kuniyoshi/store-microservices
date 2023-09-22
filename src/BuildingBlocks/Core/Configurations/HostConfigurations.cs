using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

namespace Core.Configurations
{
    public static class HostConfigurations
    {
        public static IHostBuilder ConfigureAppSettings(this IHostBuilder host)
        {
            host.ConfigureAppConfiguration((ctx, builder) =>
            {
                var enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                builder.SetBasePath(ctx.HostingEnvironment.ContentRootPath);
                builder.AddJsonFile("appsettings.json", false, true);
                builder.AddJsonFile($"appsettings.{enviroment}.json", true, true);

                builder.AddEnvironmentVariables();
            });

            return host;
        }

        public static WebApplicationBuilder ConfigureAppSettings(this WebApplicationBuilder webAppBuilder)
        {
            var enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(webAppBuilder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{enviroment}.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            webAppBuilder.Configuration.AddConfiguration(configuration);

            return webAppBuilder;
        }

        public static IHostBuilder AddSerilog(this IHostBuilder hostBuilder)
        {
            if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                hostBuilder.UseSerilog((hostingContext, loggerConfiguration) =>
                {
                    loggerConfiguration
                        .Enrich.FromLogContext()
                        .Enrich.WithMachineName()
                        .WriteTo.Console()
                        .WriteTo.Debug()
                        .Enrich.WithProperty("Environment", hostingContext.HostingEnvironment.EnvironmentName)
                        .Enrich.WithProperty("ApplicationName", hostingContext.Configuration.GetValue<string>("Application:Name")!)
                        .ReadFrom.Configuration(hostingContext.Configuration);
                });
            }
            else
            {
                hostBuilder.UseSerilog((hostingContext, loggerConfiguration) =>
                {
                    loggerConfiguration
                        .Enrich.FromLogContext()
                        .Enrich.WithMachineName()
                        .WriteTo.Console()
                        .WriteTo.Elasticsearch(ConfigureElasticSink(hostingContext.Configuration, hostingContext.HostingEnvironment.EnvironmentName))
                        .Enrich.WithProperty("Environment", hostingContext.HostingEnvironment.EnvironmentName)
                        .Enrich.WithProperty("ApplicationName", hostingContext.Configuration.GetValue<string>("Aplication:Name")!)
                        .ReadFrom.Configuration(hostingContext.Configuration);
                });
            }
            

            return hostBuilder;
        }

        private static ElasticsearchSinkOptions ConfigureElasticSink(IConfiguration configuration, string environment)
        {
            return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name?.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
            };
        }
    }
}
