using EmptyStore.Contexts;
using EmptyStore.Entities;
using EmptyStore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmptyStore.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ShopContext _context;

        public RegistrationController(ShopContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Registration");
        }

        [HttpPost]
        public IActionResult Create(RegistrationViewModel registration)
        {
            if (ModelState.IsValid == false)
            {
                return Index();
            }

            Person? person = _context.Persons.FirstOrDefault(p => p.Email == registration.Email);

            if (person != null)
            {
                ModelState.AddModelError("", "Email уже используется");
                return RedirectToAction("Index");
            }

            person = new Person()
            {
                Email = registration.Email,
                Password = registration.Password,
            };

            _context.Persons.Add(person);
            _context.SaveChanges();

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, person.Id.ToString()) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return Redirect("~/products");
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckEmail(string email)
        {
            Person? person = _context.Persons.FirstOrDefault(p => p.Email == email);
            if (person == null)
            {
                return Json(true);
            }

            return Json(false);
        }
    }
}
