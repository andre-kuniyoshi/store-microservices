using Register.Data.Extensions;
using Register.Application.Extensions;
using Order.Infra.Data.Seed;
using Core.Configurations;

namespace Register.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddApplicationLayer();
            builder.Services.AddInfraLayer(builder.Configuration);

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddControllers().AddTestController();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSecurityConfigs(builder.Configuration);

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


            app.MapControllers();

            app.Run();
        }
    }
}