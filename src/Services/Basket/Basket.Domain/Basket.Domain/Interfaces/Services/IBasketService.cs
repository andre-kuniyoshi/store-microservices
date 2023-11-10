using Basket.Domain.Entities;

namespace Basket.Domain.Interfaces.Services
{
    public interface IBasketService
    {
        public Task<ShoppingCart> GetBasket(Guid clientId);
        public Task<ShoppingCart> CreateUpdateBasket(ShoppingCart basket);
        public Task DeleteBasket(Guid clientId);
        public Task CheckoutBasket(BasketCheckout basketCheckout);
    }
}
