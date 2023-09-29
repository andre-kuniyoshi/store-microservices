using AspNetCoreMVC.Models;

namespace AspNetCoreMVC.Services.Interfaces
{
    public interface IBasketService
    {
        Task<BasketModel> GetBasket();
        Task<int> GetBasketItemsCount();
        Task<BasketModel> UpdateBasket(BasketModel model);
        Task CheckoutBasket(BasketCheckoutModel model);
    }
}
