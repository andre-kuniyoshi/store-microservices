using Basket.Domain.Interfaces.Repositories;
using Basket.Domain.Interfaces.Services;
using Basket.Domain.Interfaces.GrpcServiceClients;
using Basket.Domain.Interfaces.MessageQueue;
using Basket.Domain.Entities;

namespace Basket.Application.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IInventoryGrpcServiceClient _inventoryGrpcClient;
        private readonly IPublishEvents _publishEndpoint;

        public BasketService(IBasketRepository basketRepo, IInventoryGrpcServiceClient inventoryGrpcClient, IPublishEvents publishEndpoint)
        {
            _basketRepo = basketRepo;
            _inventoryGrpcClient = inventoryGrpcClient;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<ShoppingCart> GetBasket(Guid userId)
        {
            var basket = await _basketRepo.GetBasket(userId);
            return basket;
        }

        public async Task<ShoppingCart> CreateUpdateBasket(ShoppingCart basket)
        {
            foreach (var item in basket.Items)
            {
                var product = await _inventoryGrpcClient.GetProductPrice(item.ProductId);

                item.Price = product.Price;
                if(product.Sale != null)
                {
                    item.SalePrice = product.Sale.Price;
                }
            }

            var basketAdded = await _basketRepo.CreateUpdateBasket(basket);
            return basketAdded;
        }

        public async Task DeleteBasket(Guid userId)
        {
            await _basketRepo.DeleteBasket(userId);       
        }

        public async Task CheckoutBasket(BasketCheckout basketCheckout)
        {
            var shoppingCart = await _basketRepo.GetBasket(basketCheckout.ClientId);
            if (shoppingCart == null)
            {
                return;
            }
            basketCheckout.Products = shoppingCart.Items;

            // send checkout event to rabbitmq
            var publishSuccess = await _publishEndpoint.PublishCheckoutEvent(basketCheckout);
            if(publishSuccess)
            {
                return;
            }

            // remove the basket
            await _basketRepo.DeleteBasket(basketCheckout.ClientId);
        }
    }
}
