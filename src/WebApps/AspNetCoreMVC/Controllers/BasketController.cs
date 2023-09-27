using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMVC.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult AddProductu(int productId)
        {
            return View();
        }
    }
}
