using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Data.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;
        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public async Task<ShoppingCart?> GetBasket(Guid clientId)
        {
            var basket = await _redisCache.GetStringAsync(clientId.ToString());

            if (String.IsNullOrEmpty(basket)) return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart?> CreateUpdateBasket(ShoppingCart basket)
        {
            var options = new DistributedCacheEntryOptions();
            options.SetSlidingExpiration(TimeSpan.FromHours(1));

            await _redisCache.SetStringAsync(basket.ClientId.ToString(), JsonConvert.SerializeObject(basket), options);

            return await GetBasket(basket.ClientId);
        }

        public async Task DeleteBasket(Guid clientId)
        {
            await _redisCache.RemoveAsync(clientId.ToString());
        }
    }
}
