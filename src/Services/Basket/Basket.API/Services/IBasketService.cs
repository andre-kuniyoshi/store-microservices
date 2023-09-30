using Basket.API.Entities;

namespace Basket.API.Services
{
    public interface IBasketService
    {
        public Task<ShoppingCart> GetBasket(Guid userId);
        public Task<ShoppingCart> CreateUpdateBasket(ShoppingCart basket);
        public Task DeleteBasket(string userName);
        public Task CheckoutBasket(Guid userId, BasketCheckout basketCheckout);
    }
}
