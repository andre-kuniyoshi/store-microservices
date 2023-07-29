using Catalog.API.Entities;

namespace Catalog.API.Services
{
    public interface ICatalogService
    {
        public Task<List<Product>> GetProducts();
        public Task<Product?> GetProductById(string id);
        public Task<List<Product>> GetProductsByCategory(string category);
        public Task AddNewProduct(Product product);
        public Task<bool> UpdateProduct(Product product);
        public Task<bool> DeleteProduct(string id);
    }
}
