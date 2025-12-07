using HomeApi.Domains.Users.Entities;
using HomeApi.Domains.Users.Repositories;
using HomeApi.Domains.Users.Requests;

namespace HomeApi.Domains.Users.Services;


public class UserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public Task<List<User>> GetAllAsync() => _repository.GetAllAsync();

    public Task<User?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

    public async Task<User> CreateAsync(CreateUserRequest request)
    {
        var user = new User
        {
            Name = request.Name,
            Email = request.Email
        };

        return await _repository.CreateAsync(user);
    }

    public async Task<User?> UpdateAsync(int id, UpdateUserRequest request)
    {
        var user = new User
        {
            Id = id,
            Name = request.Name,
            Email = request.Email
        };

        return await _repository.UpdateAsync(user);
    }

    public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
}