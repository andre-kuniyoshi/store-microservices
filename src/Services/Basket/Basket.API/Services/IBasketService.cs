﻿using Basket.API.Entities;

namespace Basket.API.Services
{
    public interface IBasketService
    {
        public Task<ShoppingCart> GetBasket(Guid clientId);
        public Task<ShoppingCart> CreateUpdateBasket(ShoppingCart basket);
        public Task DeleteBasket(Guid clientId);
        public Task CheckoutBasket(BasketCheckout basketCheckout);
    }
}
