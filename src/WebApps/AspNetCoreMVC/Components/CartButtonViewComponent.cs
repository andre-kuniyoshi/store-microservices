using AspNetCoreMVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMVC.Components
{
    public class CartButtonViewComponent : ViewComponent
    {
        private readonly IBasketService _basketService;

        public CartButtonViewComponent(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cartCount = await _basketService.GetBasketItemsCount();

            return View(cartCount);
        }
    }
}
