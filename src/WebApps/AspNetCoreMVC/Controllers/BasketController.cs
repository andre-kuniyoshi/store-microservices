using AspNetCoreMVC.Models;
using AspNetCoreMVC.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMVC.Controllers
{
    public class BasketController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;

        public BasketController(ICatalogService catalogService, IBasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var basket = await _basketService.GetBasket();

            return View(basket);
        }

        [Authorize]
        public async Task<IActionResult> AddProduct(string productObjectId)
        {
            try
            {
                var product = await _catalogService.GetProduct(productObjectId);

                var basket = await _basketService.GetBasket();

                basket.Items.Add(new BasketItemModel
                {
                    ProductId = product.Id,
                    ProductObjectId = product.ObjectId,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = 1,
                    Color = "Black"
                });

                var basketUpdated = await _basketService.UpdateBasket(basket);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
    }
}
