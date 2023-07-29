using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infra.Data.Context
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order() {UserName = "ahk", FirstName = "Andre", LastName = "Kuniyoshi", EmailAddress = "andre@kuniyoshi.com", AddressLine = "One", Country = "Brazil", TotalPrice = 350 }
            };
        }
    }
}
