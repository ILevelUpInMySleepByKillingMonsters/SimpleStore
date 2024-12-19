using Business.Abstractions.Services;
using Data;
using Data.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Business.Services
{
    public class CartService : ICartService
    {
        private readonly DatabaseContext _context;

        public CartService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task AddAmount(int cartItemId)
        {
            CartItem? cartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.Id == cartItemId);

            if (cartItem == null)
            {
                return;
            }

            cartItem.Quantity++;
            await _context.SaveChangesAsync();
        }

        public async Task AddToCart(Product product, int userId)
        {
            CartItem? cartItem = await _context.CartItems
               .FirstOrDefaultAsync(c => c.ProductId == product.Id && c.PersonId == userId);

            if (cartItem != null)
            {
                return;
            }

            cartItem = new CartItem()
            {
                Quantity = 1,
                PersonId = userId,
                ProductId = product.Id
            };

            await _context.CartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int cartItemId)
        {
            CartItem? cartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.Id == cartItemId);

            if (cartItem == null)
            {
                return;
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CartItem>> GetAllByUserId(int userId)
        {
            List<CartItem> cartItems = await _context.CartItems
                .Where(c => c.PersonId == userId)
                .Include(c => c.Product)
                .OrderBy(c => c.Id)
                .ToListAsync();
            Console.WriteLine(cartItems.Count);
            return cartItems;
        }

        public async Task<float> GetTotalPrice(int userId)
        {
            return await _context.CartItems
                .Where(c => c.PersonId == userId)
                .Include(c => c.Product)
                .SumAsync(c => c.Product.Price * c.Quantity);
        }

        public async Task Order(int userId)
        {
            var cartItems = await _context.CartItems
                .Where(c => c.PersonId == userId)
                .ToListAsync();
            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
        }

        public async Task SubAmount(int cartItemId)
        {
            CartItem? cartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.Id == cartItemId);

            if (cartItem == null)
            {
                return;
            }

            if (cartItem.Quantity <= 1)
            {
                return;
            }

            cartItem.Quantity--;
            await _context.SaveChangesAsync();
        }
    }
}
