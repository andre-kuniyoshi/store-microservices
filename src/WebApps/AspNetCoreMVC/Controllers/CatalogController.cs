using AspNetCoreMVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMVC.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService _catalogService;
        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
