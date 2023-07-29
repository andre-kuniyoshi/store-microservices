using Basket.API.Entities;

namespace Basket.API.Services
{
    public interface IBasketService
    {
        public Task<ShoppingCart> GetBasket(string userName);
        public Task<ShoppingCart> CreateUpdateBasket(ShoppingCart basket);
        public Task DeleteBasket(string userName);
        public Task CheckoutBasket(BasketCheckout basketCheckout);
    }
}
