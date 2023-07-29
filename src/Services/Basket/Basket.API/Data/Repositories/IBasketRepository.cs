using Basket.API.Entities;

namespace Basket.API.Data.Repositories
{
    public interface IBasketRepository
    {
        public Task<ShoppingCart> GetBasket(string userName);
        public Task<ShoppingCart> CreateUpdateBasket(ShoppingCart basket);
        public Task DeleteBasket(string userName);
    }
}
