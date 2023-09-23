using AspNetCoreMVC.Models;

namespace AspNetCoreMVC.Services.Interfaces
{
    public interface ICatalogService
    {
        public Task<IEnumerable<Product>> GetProducts();

        public Task<Product> GetProduct(Guid id);

        public Task<List<Product>> GetCatalogByCategory(string category);

        public Task<Product> CreateCatalog(Product model);
    }
}
