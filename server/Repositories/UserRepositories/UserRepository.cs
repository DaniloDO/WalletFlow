using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using server.Repositories.UserRepositories.Interfaces;

namespace server.Repositories.UserRepositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context; 

    public UserRepository(AppDbContext context)
    {
        _context = context; 
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        return await _context.Users.ToListAsync(); 
    }

    public async Task<User?> GetUser(Guid publicId)
    {
        var user = await _context.Users
            .Include(u => u.Transactions)
            .FirstOrDefaultAsync(u => u.PublicId == publicId); 

        return user; 
    }

    public async Task CreateUser(User user)
    {
        _context.Users.Add(user); 
    }

    public async Task UpdateUser(User user)
    {
        _context.Users.Update(user); 
    }

    public async Task DeleteUser(User user)
    {
        _context.Users.Remove(user); 
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync(); 
    }

}
