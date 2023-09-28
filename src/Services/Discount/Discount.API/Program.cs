using Discount.API.Extensions;
using Discount.Infra.Extensions;
using Discount.Application.Extensions;
using Core.Configurations;
using Serilog;

namespace Discount.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.AddSerilog();

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddApplicationLayer();
            builder.Services.AddInfraLayer();

            builder.Services.AddSwaggerConfigs(builder.Environment);

            var app = builder.Build();

            app.MigrateDatabase<Program>();

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