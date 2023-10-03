using AutoMapper;
using Basket.API.Data.Repositories;
using Basket.API.Entities;
using Basket.API.GrpcServicesClients;
using EventBus.Messages.Events;
using MassTransit;

namespace Basket.API.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly ProductGrpcService _inventoryGrpcClient;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public BasketService(IBasketRepository basketRepo, ProductGrpcService inventoryGrpcClient, IMapper mapper, IPublishEndpoint publishEndpoint)
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

        public async Task DeleteBasket(string userName)
        {
            await _basketRepo.DeleteBasket(userName);       
        }

        public async Task CheckoutBasket(Guid userId, BasketCheckout basketCheckout)
        {
            var basket = await _basketRepo.GetBasket(basketCheckout.Id);
            if (basket == null)
            {
                return;
            }

            // send checkout event to rabbitmq
            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.TotalPrice = basket.TotalPrice;
            await _publishEndpoint.Publish<BasketCheckoutEvent>(eventMessage);

            // remove the basket
            //await _basketRepo.DeleteBasket(basket.UserName);
        }
    }
}
