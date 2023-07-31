using Core.Configurations;
using Core.DependencyInjectionExtension;
using Serilog;

namespace Basket.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            HostConfigurations.ConfigureSerilog();
            builder.Host.UseSerilog();

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddDependencyInjection(builder.Configuration);
            builder.Services.AddCoreLib();
            builder.Services.AddStackExchangeRedisCache(options => 
            {
                options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}