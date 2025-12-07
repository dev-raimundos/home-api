using HomeApi.Domains.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeApi.Domains.Users.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DbContext _context;

    public UserRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Set<User>().ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Set<User>().FindAsync(id);
    }

    public async Task<User> CreateAsync(User user)
    {
        _context.Set<User>().Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateAsync(User user)
    {
        var exists = await _context.Set<User>().FindAsync(user.Id);
        if (exists == null) return null;

        exists.Name = user.Name;
        exists.Email = user.Email;

        await _context.SaveChangesAsync();
        return exists;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _context.Set<User>().FindAsync(id);
        if (user == null) return false;

        _context.Set<User>().Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}