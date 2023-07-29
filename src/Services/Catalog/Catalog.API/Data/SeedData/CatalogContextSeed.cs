using Catalog.API.Entities;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.API.Data.SeedData
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetPreconfiguredProducts());
            }
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            var productsData = File.ReadAllText("Data/SeedData/Products.json");
            var products = JsonSerializer.Deserialize<IEnumerable<Product>>(productsData);
            return products;
        }
    }
}
