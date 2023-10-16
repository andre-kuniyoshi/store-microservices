using AspNetCoreMVC.Models;
using AspNetCoreMVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMVC.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        private readonly IRegisterService _registerService;
        public AccountController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _registerService.GetUserInfos();
            return View(result);
            //return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserModel userModel)
        {
            // TODO: Add validator
            try
            {
                var result = await _registerService.UpdateUserInfos(userModel);

                return RedirectToAction("Index", "/");
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}
