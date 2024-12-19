using Business.Abstractions.Services;
using Data;
using Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Endpoint.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ILoginService _loginService;

        public CartController(ILoginService loginService, ICartService cartService)
        {
            _cartService = cartService;
            _loginService = loginService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int userId = _loginService.GetUserId(HttpContext);
            List<CartItem> cartItems = await _cartService.GetAllByUserId(userId);
            ViewBag.TotalPrice = await _cartService.GetTotalPrice(userId);
            return View("Cart", cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int cartItemId)
        {
            await _cartService.Delete(cartItemId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Add(int cartItemId)
        {
            await _cartService.AddAmount(cartItemId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Sub(int cartItemId)
        {
            await _cartService.SubAmount(cartItemId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Order()
        {
            await _cartService.Order(_loginService.GetUserId(HttpContext));

            return RedirectToAction("Index");
        }
    }
}
