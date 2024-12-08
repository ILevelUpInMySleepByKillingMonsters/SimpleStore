using EmptyStore.Contexts;
using EmptyStore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmptyStore.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ShopContext _context;

        public CartController(ShopContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            int personId = Int32.Parse(HttpContext.User.Identity.Name);
            var cartItems = _context.CartItems
                .Where(c => c.PersonId == personId)
                .Include(c => c.Product)
                .OrderBy(c => c.Id)
                .ToList();
            ViewBag.TotalPrice = GetTotalPrice(personId);
            return View("Cart", cartItems);
        }

        [HttpPost]
        public IActionResult Delete(int cartItemId)
        {
            CartItem? cartItem = _context.CartItems.FirstOrDefault(c => c.Id == cartItemId);

            if (cartItem == null)
            {
                return NotFound();
            }

            _context.CartItems.Remove(cartItem);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Add(int cartItemId)
        {
            CartItem? cartItem = _context.CartItems.FirstOrDefault(c => c.Id == cartItemId);

            if (cartItem == null)
            {
                return NotFound();
            }

            cartItem.Quantity++;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Sub(int cartItemId)
        {
            CartItem? cartItem = _context.CartItems.FirstOrDefault(c => c.Id == cartItemId);

            if (cartItem == null)
            {
                return NotFound();
            }

            if (cartItem.Quantity <= 1)
            {
                return RedirectToAction("Index");
            }

            cartItem.Quantity--;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Order()
        {
            var cartItems = _context.CartItems
                .Where(c => c.PersonId == Int32.Parse(HttpContext.User.Identity.Name))
                .ToList();
            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public string GetTotalPrice(int personId)
        {
            int sum = (int)_context.CartItems
                .Where(c => c.PersonId == personId)
                .Include(c => c.Product)
                .Sum(c => c.Product.Price * c.Quantity);
            return sum.ToString();
        }
    }
}
