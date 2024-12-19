using Business.Abstractions.Services;
using Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Endpoint.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;

        public ProductsController(ILoginService loginService, ICartService cartService, IProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
            _loginService = loginService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProducts();
            return View("Products", products);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            int userId = _loginService.GetUserId(HttpContext);
            Product? product = await _productService.GetProduct(productId);
            if (product == null)
            {
                return RedirectToAction("Index");
            }

            await _cartService.AddToCart(product, userId);

            return RedirectToAction("Index");
        }
    }
}
