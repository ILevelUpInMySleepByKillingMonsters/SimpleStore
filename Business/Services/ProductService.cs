using Business.Abstractions.Services;
using Data;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ProductService : IProductService
    {
        private readonly DatabaseContext _context;

        public ProductService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProduct(int productId)
        {
            Product? product = await _context.Products.FirstOrDefaultAsync(c => c.Id == productId);

            return product;
        }

        public async Task<List<Product>> GetProducts()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return products;
        }
    }
}
