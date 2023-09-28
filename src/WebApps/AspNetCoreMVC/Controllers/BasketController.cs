using AspNetCoreMVC.Models;
using AspNetCoreMVC.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Client.AspNetCore;

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

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> AddProduct(string productObjectId)
        {
            try
            {
                var token = await HttpContext.GetTokenAsync(OpenIddictClientAspNetCoreConstants.Tokens.BackchannelAccessToken);

                Console.WriteLine("Token: " + token);
                var req = Request.Headers;
                var user = User.Identity;
                var product = await _catalogService.GetProduct(productObjectId);

                var userName = "ahk";
                var basket = await _basketService.GetBasket(userName);

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
                return View(basketUpdated);
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
    }
}
