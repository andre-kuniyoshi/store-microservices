using Catalog.API.Data.SeedData;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data.Context
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }
        private readonly IConfiguration _config;

        public CatalogContext(IConfiguration config)
        {
            _config = config;
            var client = new MongoClient(_config.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(_config.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<Product>(_config.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogContextSeed.SeedData(Products);
        }

    }
}
