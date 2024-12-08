using EmptyStore.Contexts;
using EmptyStore.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmptyStore.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ShopContext _context;

        public ProductsController(ShopContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View("Products", products);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            int personId = Int32.Parse(HttpContext.User.Identity.Name);
            CartItem? cartItem = _context.CartItems
               .FirstOrDefault(c => c.ProductId == productId && c.PersonId == personId);

            if (cartItem == null)
            {
                cartItem = new CartItem()
                {
                    Quantity = 1,
                    PersonId = personId,
                    ProductId = productId
                };
                _context.CartItems.Add(cartItem);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
