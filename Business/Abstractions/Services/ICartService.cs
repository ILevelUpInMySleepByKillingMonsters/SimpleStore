using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstractions.Services
{
    public interface ICartService
    {
        Task AddToCart(Product product, int userId);
        Task<List<CartItem>> GetAllByUserId(int userId);
        Task AddAmount(int cartItemId);
        Task SubAmount(int cartItemId);
        Task Delete(int cartItemId);
        Task Order(int userId);
        Task<float> GetTotalPrice(int userId);
    }
}
