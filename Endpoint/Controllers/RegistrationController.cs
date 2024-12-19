using Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Business.Abstractions.Services;
using Data;
using Endpoint.ViewModels;

namespace Endpoint.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly ILoginService _loginService;

        public RegistrationController(ILoginService loginService, IUserManager userManager)
        {
            _userManager = userManager;
            _loginService = loginService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Registration");
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegistrationViewModel registration)
        {
            if (ModelState.IsValid == false)
            {
                return Index();
            }

            bool userExist = await _userManager.IsExistsByEmailAsync(registration.Email);

            if (userExist == true)
            {
                ModelState.AddModelError("", "Email уже используется");
                return RedirectToAction("Index");
            }

            User user = new User()
            {
                Email = registration.Email,
                Password = registration.Password,
                Role = "User"
            };

            await _userManager.CreateUser(user);

            _loginService.SignIn(HttpContext, user.Id.ToString());
            return Redirect("~/products");
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> CheckEmail(string email)
        {
            bool userExist = await _userManager.IsExistsByEmailAsync(email);
            if (userExist == false)
            {
                return Json(true);
            }

            return Json(false);
        }
    }
}
