using HomeApi.Domains.Users.Entities;

namespace HomeApi.Domains.Users.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User> CreateAsync(User user);
        Task<bool> EmailExistsAsync(string email);
    }
}
