using Catalog.API.Data.Repositories;
using Catalog.API.Entities;
using Core.NotifierErrors;

namespace Catalog.API.Services
{
    public class CatalogService : BaseService, ICatalogService
    {
        private readonly IProductRepository _productRepo;

        public CatalogService(IProductRepository productRepo, INotifier notifier) : base(notifier)
        {
            _productRepo = productRepo;
        }

        public async Task<Product?> GetProductById(string id)
        {
            var product =  await _productRepo.GetProduct(id);

            if(product is null)
            {
                NotifyErrorMessage("Id", "Product not found.");
                return null;
            }

            return product;
        }

        public async Task<List<Product>> GetProducts()
        {
            var products = await _productRepo.GetProducts();
            return products.ToList();
        }

        public async Task<List<Product>> GetProductsByCategory(string category)
        {
            var products = await _productRepo.GetProductsByCategory(category);
            return products.ToList();
        }

        public async Task AddNewProduct(Product product)
        {
            await _productRepo.CreateProduct(product);
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _productRepo.UpdateProduct(product);

            return updateResult;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var deleteResult = await _productRepo.DeleteProduct(id);

            return deleteResult;
        }
    }
}
