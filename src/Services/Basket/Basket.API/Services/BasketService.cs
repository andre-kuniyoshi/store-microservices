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
        private readonly DiscountGrpcServiceClient _discountGrpcClient;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public BasketService(IBasketRepository basketRepo, DiscountGrpcServiceClient discountGrpcClient, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _basketRepo = basketRepo;
            _discountGrpcClient = discountGrpcClient;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await _basketRepo.GetBasket(userName);
            return basket;
        }

        public async Task<ShoppingCart> CreateUpdateBasket(ShoppingCart basket)
        {
            foreach (var item in basket.Items)
            {
                var coupon = await _discountGrpcClient.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }

            var basketAdded = await _basketRepo.CreateUpdateBasket(basket);
            return basketAdded;
        }

        public async Task DeleteBasket(string userName)
        {
            await _basketRepo.DeleteBasket(userName);       
        }

        public async Task CheckoutBasket(BasketCheckout basketCheckout)
        {
            var basket = await _basketRepo.GetBasket(basketCheckout.UserName);
            if (basket == null)
            {
                return;
            }

            // send checkout event to rabbitmq
            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.TotalPrice = basket.TotalPrice;
            await _publishEndpoint.Publish<BasketCheckoutEvent>(eventMessage);

            // remove the basket
            await _basketRepo.DeleteBasket(basket.UserName);
        }
    }
}
