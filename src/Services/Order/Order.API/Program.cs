using EventBus.Messages.Common;
using MassTransit;
using Order.API.EventBusConsumer;
using Order.API.Extensions;
using Order.Application.Extensions;
using Order.Infra.Extensions;
using Serilog;
using Core.Configurations;
using Order.Infra.Data.Seed;

namespace Order.API
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
            builder.Services.AddApplicationLayer();
            builder.Services.AddInfraLayer(builder.Configuration);
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddScoped<BasketCheckoutConsumer>();

            builder.Services.AddMassTransit(config => {

                config.AddConsumer<BasketCheckoutConsumer>();

                config.UsingRabbitMq((ctx, cfg) => {
                    cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);

                    cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c => {
                        c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
                    });
                });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.Lifetime.ApplicationStarted.Register(() => OrderContextSeed.MigrateDatabase(app));

            // Seed Database
            //app.MigrateDatabase<OrderContext>((context, service) =>
            //{
            //    var logger = service.GetService<ILogger<OrderContextSeed>>();
            //    OrderContextSeed.SeedAsync(context, logger).Wait();
            //});

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