using Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Data;
using Endpoint.ViewModels;
using Business.Abstractions.Services;

namespace Endpoint.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService, IUserManager userManager)
        {
            _userManager = userManager;
            _loginService = loginService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel login)
        {
            if (ModelState.IsValid == false)
            {
                return RedirectToAction("Index");
            }

            User? user = await _userManager.GetByEmailPasswordAsync(login.Email, login.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Вы ввели неверное имя пользователя или неверный пароль");
                return Index();
            }


            _loginService.SignIn(HttpContext, user.Id.ToString());

            return Redirect("~/products");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            _loginService.SignOut(HttpContext);
            return Redirect("~/");
        }
    }
}
