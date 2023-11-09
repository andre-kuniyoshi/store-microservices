using AutoMapper;
using Basket.API.Entities;
using Basket.Domain.Interfaces.Repositories;
using Basket.Domain.Interfaces.Services;
using EventBus.Messages.Events;
using MassTransit;
using Basket.Domain.Interfaces.GrpcServiceClients;

namespace Basket.Application.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IInventoryGrpcServiceClient _inventoryGrpcClient;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public BasketService(IBasketRepository basketRepo, IInventoryGrpcServiceClient inventoryGrpcClient, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _basketRepo = basketRepo;
            _inventoryGrpcClient = inventoryGrpcClient;
            _mapper = mapper;
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
            var basket = await _basketRepo.GetBasket(basketCheckout.ClientId);
            if (basket == null)
            {
                return;
            }

            // send checkout event to rabbitmq
            var products = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.TotalPrice = basket.TotalPrice;
            await _publishEndpoint.Publish<BasketCheckoutEvent>(eventMessage);

            // remove the basket
            await _basketRepo.DeleteBasket(basketCheckout.ClientId);
        }
    }
}
