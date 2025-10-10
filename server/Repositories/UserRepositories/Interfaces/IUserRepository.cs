using server.Models; 

namespace server.Repositories.UserRepositories.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsers(); 
    Task<User?> GetUser(Guid publicId); 
    Task CreateUser(User user);
    Task UpdateUser(User user); 
    Task DeleteUser(User user);
    Task SaveChangesAsync();  
}
