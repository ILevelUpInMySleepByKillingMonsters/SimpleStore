using Business.Abstractions.Services;
using Data;
using Data.Entity;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Text;

namespace Business.Services
{
    public class UserManager : IUserManager
    {
        private readonly DatabaseContext _context;

        public UserManager(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(User user)
        {
            user.Password = HashPassword(user.Password);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User?> GetByIdPasswordAsync(long id, string password)
        {
            var hash = HashPassword(password);
            return await GetAsync(user => user.Id == id && user.Password == hash);
        }

        public async Task<User?> GetByEmailPasswordAsync(string email, string password)
        {
            var hash = HashPassword(password);
            return await GetAsync(user => user.Email == email && user.Password == hash);
        }

        public async Task<bool> IsExistsByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(it => it.Email == email);
        }

        private async Task<User?> GetAsync(Expression<Func<User, bool>> predicate)
        {
            return await _context.Users.FirstOrDefaultAsync(predicate);
        }

        private string HashPassword(string password)
        {
            var salt = Encoding.UTF8.GetBytes("SecurityKey");
            return Convert.ToHexString(KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, 100000, 32));
        }
    }
}
