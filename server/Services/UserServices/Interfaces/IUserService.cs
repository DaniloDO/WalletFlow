using System;
using server.DTOs;
using server.Models;

namespace server.Services.UserServices.Interfaces;

public interface IUserService
{
    Task<IEnumerable<SimpleUserReadDTO>> GetUsers();
    Task<UserReadDTO?> GetUser(Guid publicId);
    Task<UserReadDTO> CreateUser(UserWriteDTO dto);
    Task<bool> UpdateUser(Guid publicId, UserWriteDTO dto);
    Task<bool> DeleteUser(Guid publicId); 
}
