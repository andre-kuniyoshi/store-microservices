using AspNetCoreMVC.Services.Interfaces;
using AspNetCoreMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;
        public HomeController(IHttpClientFactory httpClientFactory, ICatalogService catalogService, IBasketService basketService, IConfiguration config)
        {
            _catalogService = catalogService;
            _basketService = basketService;
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _catalogService.GetProducts();
            var homeViewModel = new HomeViewModel
            {
 
                Products = products
            };

            if(User.Identity != null && User.Identity.IsAuthenticated)
            {
                ViewBag.CartItemsCount = await _basketService.GetBasketItemsCount();
            }
            //else
            //{
            //    ViewBag.CartItemsCount = 0;
            //}

 
            return View(model: homeViewModel);
        }

        //[Authorize, HttpPost("~/")]
        //public async Task<ActionResult> Index(CancellationToken cancellationToken)
        //{
        //    // For scenarios where the default authentication handler configured in the ASP.NET Core
        //    // authentication options shouldn't be used, a specific scheme can be specified here.
        //    var token = await HttpContext.GetTokenAsync(OpenIddictClientAspNetCoreConstants.Tokens.BackchannelAccessToken);

        //    Console.WriteLine("Token: " + token);
        //    using var client = _httpClientFactory.CreateClient();

        //    using var request = new HttpRequestMessage(HttpMethod.Get, $"{_config.GetValue<string>("ApiSettings:GatewayAddress")}/Register");
        //    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //    using var response = await client.SendAsync(request, cancellationToken);
        //    response.EnsureSuccessStatusCode();

        //    return View(model: await response.Content.ReadAsStringAsync(cancellationToken));
        //}
    }
}