using AspNetCoreMVC.Models;
using AspNetCoreMVC.Services.Interfaces;
using AspNetCoreMVC.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Client.AspNetCore;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace AspNetCoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        private readonly ICatalogService _catalogService;
        public HomeController(IHttpClientFactory httpClientFactory, ICatalogService catalogService, IConfiguration config)
        {
            _catalogService = catalogService;
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

            //return View();
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