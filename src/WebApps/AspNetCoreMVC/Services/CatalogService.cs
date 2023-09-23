using AspNetCoreMVC.Models;
using AspNetCoreMVC.Services.Interfaces;
using Core.Extensions;

namespace AspNetCoreMVC.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;

        public CatalogService(HttpClient client, ILogger<CatalogService> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var response = await _client.GetAsync("/Catalog");
            return await response.ReadContentAs<IEnumerable<Product>>();
        }

        public async Task<Product> GetProduct(Guid id)
        {
            var response = await _client.GetAsync("/Catalog");
            return await response.ReadContentAs<Product>();
        }

        public async Task<List<Product>> GetCatalogByCategory(string category)
        {
            var response = await _client.GetAsync($"/Catalog/GetProductByCategory/{category}");
            return await response.ReadContentAs<List<Product>>();
        }

        public async Task<Product> CreateCatalog(Product model)
        {
            var response = await _client.PostAsJson($"/Catalog", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<Product>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }
    }
}
