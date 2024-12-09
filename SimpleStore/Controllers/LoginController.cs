using EmptyStore.Entities;
using EmptyStore.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using EmptyStore.Contexts;
using SimpleStore;
using System;

namespace EmptyStore.Controllers
{
    public class LoginController : Controller
    {
        private readonly ShopContext _context;
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService, ShopContext context)
        {
            _context = context;
            _loginService = loginService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel user)
        {
            if (ModelState.IsValid == true)
            {
                Person? person = _context.Persons.FirstOrDefault(p => p.Email == user.Email && p.Password == user.Password);
                if (person == null)
                {
                    ModelState.AddModelError("", "Вы ввели неверное имя пользователя или неверный пароль");
                    return Index();
                }


                _loginService.SignIn(HttpContext, person.Id.ToString());
                //var claims = new List<Claim> { new Claim(ClaimTypes.Name, person.Id.ToString()) };
                //ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                //HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return Redirect("~/products");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            _loginService.SignOut(HttpContext);
            //HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("~/");
        }
    }
}
