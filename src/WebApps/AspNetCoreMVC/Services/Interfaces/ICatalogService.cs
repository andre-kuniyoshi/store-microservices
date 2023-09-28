using AspNetCoreMVC.Models;

namespace AspNetCoreMVC.Services.Interfaces
{
    public interface ICatalogService
    {
        public Task<IEnumerable<ProductModel>> GetProducts();

        public Task<ProductModel> GetProduct(string objecId);

        public Task<List<ProductModel>> GetCatalogByCategory(string category);

        public Task<ProductModel> CreateCatalog(ProductModel model);
    }
}
