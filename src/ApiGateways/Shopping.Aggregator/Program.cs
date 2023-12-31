using Core.Configurations;
using Serilog;
using Shopping.Aggregator.Services;

namespace Shopping.Aggregator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.AddSerilog();

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddSwaggerConfigs(builder.Environment);

            builder.Services.AddHttpClient<ICatalogService, CatalogService>(c =>
                c.BaseAddress = new Uri(builder.Configuration["ApiSettings:CatalogUrl"]));
                //.AddHttpMessageHandler<LoggingDelegatingHandler>()
                //.AddPolicyHandler(GetRetryPolicy())
                //.AddPolicyHandler(GetCircuitBreakerPolicy());

            builder.Services.AddHttpClient<IBasketService, BasketService>(c =>
                c.BaseAddress = new Uri(builder.Configuration["ApiSettings:BasketUrl"]));
            //.AddHttpMessageHandler<LoggingDelegatingHandler>()
            //.AddPolicyHandler(GetRetryPolicy())
            //.AddPolicyHandler(GetCircuitBreakerPolicy());

            builder.Services.AddHttpClient<IOrderService, OrderService>(c =>
                c.BaseAddress = new Uri(builder.Configuration["ApiSettings:OrderingUrl"]));
                //.AddHttpMessageHandler<LoggingDelegatingHandler>()
                //.AddPolicyHandler(GetRetryPolicy())
                //.AddPolicyHandler(GetCircuitBreakerPolicy());

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