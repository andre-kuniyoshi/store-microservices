using Basket.API.Entities;

namespace Basket.API.Data.Repositories
{
    public interface IBasketRepository
    {
        public Task<ShoppingCart?> GetBasket(Guid userId);
        public Task<ShoppingCart?> CreateUpdateBasket(ShoppingCart basket);
        public Task DeleteBasket(Guid userId);
    }
}
