using Data.Entity;

namespace Business.Abstractions.Services
{
    public interface IUserManager
    {
        Task<User> CreateUser(User user);
        Task<User?> GetByIdPasswordAsync(long id, string password);
        Task<User?> GetByEmailPasswordAsync(string email, string password);
        Task<bool> IsExistsByEmailAsync(string email);
    }
}
