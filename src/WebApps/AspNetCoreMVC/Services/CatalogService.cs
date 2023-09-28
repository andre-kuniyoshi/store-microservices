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

        public async Task<IEnumerable<ProductModel>> GetProducts()
        {
            var response = await _client.GetAsync("/Catalog");
            return await response.ReadContentAs<IEnumerable<ProductModel>>();
        }

        public async Task<ProductModel> GetProduct(string objectId)
        {
            var response = await _client.GetAsync($"/Catalog/{objectId}");
            return await response.ReadContentAs<ProductModel>();
        }

        public async Task<List<ProductModel>> GetCatalogByCategory(string category)
        {
            var response = await _client.GetAsync($"/Catalog/GetProductByCategory/{category}");
            return await response.ReadContentAs<List<ProductModel>>();
        }

        public async Task<ProductModel> CreateCatalog(ProductModel model)
        {
            var response = await _client.PostAsJson($"/Catalog", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<ProductModel>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }
    }
}
