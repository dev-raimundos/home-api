using HomeApi.Domains.Users.Entities;
using HomeApi.Domains.Users.Repositories;
using HomeApi.Domains.Users.DTOs;
using System.Security.Cryptography;
using System.Text;

namespace HomeApi.Domains.Users.Services
{
    public class UserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> RegisterAsync(RegisterRequest request)
        {
            // impedir email duplicado
            if (await _repository.EmailExistsAsync(request.Email))
                throw new Exception("Email already registered.");

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = HashPassword(request.Password)
            };

            return await _repository.CreateAsync(user);
        }

        public async Task<User?> LoginAsync(LoginRequest request)
        {
            var user = await _repository.GetByEmailAsync(request.Email);

            if (user == null)
                return null;

            if (!VerifyPassword(request.Password, user.PasswordHash))
                return null;

            return user;
        }

        private static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes);
        }

        private static bool VerifyPassword(string plain, string hash)
        {
            return HashPassword(plain) == hash;
        }
    }
}
