using Register.Data.Extensions;
using Register.Application.Extensions;
using Order.Infra.Data.Seed;
using Core.Configurations;
using Serilog;

namespace Register.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.ConfigureAppSettings();
            builder.Host.AddSerilog();

            // Add services to the container.
            builder.Services.AddApplicationLayer();
            builder.Services.AddInfraLayer(builder.Configuration);

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddControllers();

            builder.Services.AddSwaggerConfigs(builder.Environment);

            //builder.Services.AddRSAPublicKey(builder.Configuration);
            //builder.Services.AddSecurityConfigs(builder.Configuration);

            var app = builder.Build();

            app.Lifetime.ApplicationStarted.Register(() => RegistersContextSeed.MigrateDatabase(app));

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseTokenParser();

            app.MapControllers();

            Log.Information($"Starting {app.Environment.ApplicationName} - {app.Environment.EnvironmentName}.");

            app.Run();
        }
    }
}