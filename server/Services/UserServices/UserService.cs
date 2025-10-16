using server.DTOs;
using server.Models;
using server.Repositories.UserRepositories.Interfaces;
using server.Services.UserServices.Interfaces;
using Microsoft.AspNetCore.Identity; 

namespace server.Services.UserServices;

public class UserService : IUserService
{
    private readonly IUserRepository _repository; 
    private readonly IPasswordHasher<User> _passwordHasher; 

    public UserService(IUserRepository repository, IPasswordHasher<User> passwordHasher)
    {
        _repository = repository; 
        _passwordHasher = passwordHasher; 
    }

    public async Task<IEnumerable<SimpleUserReadDTO>> GetUsers()
    {
        var users = await _repository.GetUsers(); 

        return users.Select(u => new SimpleUserReadDTO(
            u.Id,
            u.PublicId,
            u.UserName,
            u.Email,
            u.CreatedAt
        )); 
    }

    public async Task<UserReadDTO> GetUser(Guid publicId)
    {
        var user = await _repository.GetUser(publicId); 
        if (user is null)
            return null; 

        var transactionsDto = user.Transactions.Select(t => new SimpleTransactionDTO(
            t.Id,
            t.PublicId,
            t.Amount,
            t.Date,
            t.Description,
            t.CategoryId,
            t.UserId
        ));

        return new UserReadDTO(
            user.Id,
            user.PublicId,
            user.UserName,
            user.Email,
            user.CreatedAt,
            transactionsDto
        ); 
    }

    public async Task<UserReadDTO> CreateUser(UserWriteDTO dto)
    {
        var user = new User
        {
            UserName = dto.UserName,
            Email = dto.Email,
            PasswordHash = dto.Password
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password); 
        await _repository.CreateUser(user); 
        await _repository.SaveChangesAsync(); 

        return new UserReadDTO(
            user.Id,
            user.PublicId,
            user.UserName,
            user.Email,
            user.CreatedAt,
            Enumerable.Empty<SimpleTransactionDTO>()
        ); 
    }

    public async Task<bool> UpdateUser(Guid publicId, UserWriteDTO dto)
    {
        var user = await _repository.GetUser(publicId);
        if(user is null)
            return false; 

        user.UserName = dto.UserName ?? user.UserName;
        user.Email = dto.Email ?? user.Email; 

        await _repository.UpdateUser(user); 
        await _repository.SaveChangesAsync(); 
        
        return true; 
    }

    public async Task<bool> DeleteUser(Guid publicId)
    {
        var user = await _repository.GetUser(publicId); 
        if(user is null)
            return false; 

        await _repository.DeleteUser(user); 
        await _repository.SaveChangesAsync();

        return true; 
    }
}
