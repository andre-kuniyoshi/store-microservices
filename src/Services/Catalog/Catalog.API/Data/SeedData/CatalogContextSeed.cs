using Catalog.API.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.API.Data.SeedData
{
    public class CatalogContextSeed
    {
        public async static Task SeedData(IConfiguration config)
        {
            var client = new MongoClient(config.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(config.GetValue<string>("DatabaseSettings:DatabaseName"));

            var products = database.GetCollection<Product>(config.GetValue<string>("DatabaseSettings:CollectionName"));

            bool existProduct = products.Find(p => true).Any();
            if (!existProduct)
            {
                await products.InsertManyAsync(GetPreconfiguredProducts());
            }
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            var productsData = File.ReadAllText("Data/SeedData/Products.json");
            var products = JsonSerializer.Deserialize<IEnumerable<Product>>(productsData);

            foreach (var item in products)
            {
                item.Active = true;
                item.CreatedBy = "Admin";
                item.CreatedDate = DateTime.Now;
            }

            return products;
        }
    }
}
