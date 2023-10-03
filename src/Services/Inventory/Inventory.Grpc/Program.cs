using Inventory.Grpc.Services;
using Inventory.Infra.Extensions;
using Inventory.Application.Extensions;
using Core.Configurations;
using Serilog;

namespace Discount.Grpc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.ConfigureAppSettings();
            builder.Host.AddSerilog();

            // Additional configuration is required to successfully run gRPC on macOS.
            // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

            // Add services to the container.
            builder.Services.AddGrpc();
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddApplicationLayer();
            builder.Services.AddInfraLayer(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<ProductGrpcService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            Log.Information($"Starting {app.Environment.ApplicationName} - {app.Environment.EnvironmentName}.");
            app.Run();
        }
    }
}