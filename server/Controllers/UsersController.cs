using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.DTos;
using server.Models;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UsersController(AppDbContext context, IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;

    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserReadDTO>>> GetUsers()
    {
        var users = await _context.Users.Select(u => new SimpleUserReadDTO(u.Id, u.PublicId, u.UserName, u.Email, u.CreatedAt))
                                        .ToListAsync();

        return Ok(users);
    }

    [HttpGet("{publicId}")]
    public async Task<ActionResult<UserReadDTO>> GetUser(Guid publicId)
    {
        var user = await _context.Users.Include(u => u.Transactions)
                                       .Where(u => u.PublicId == publicId)
                                       .FirstOrDefaultAsync();

        if (user is null)
            return NotFound();


        var transactionsDto = user.Transactions
            .Select(t => new TransactionDTO(
                t.Id,
                t.PublicId,
                t.Amount,
                t.Date,
                t.Description,
                t.CategoryId,
                t.UserId)); 

        var userDto = new UserReadDTO(
            user.Id,
            user.PublicId,
            user.UserName,
            user.Email,
            user.CreatedAt,
            transactionsDto
        );     


        return Ok(userDto);
    }

    [HttpPost]
    public async Task<ActionResult<UserReadDTO>> CreateUser(UserWriteDto writeDto)
    {
        var user = new User
        {
            UserName = writeDto.UserName,
            Email = writeDto.Email,
            PasswordHash = _passwordHasher.HashPassword(new User(), writeDto.Password)
        };

        _context.Add(user);
        await _context.SaveChangesAsync();

        var readDto = new SimpleUserReadDTO(user.Id, user.PublicId, user.UserName, user.Email, user.CreatedAt);
        return CreatedAtAction(nameof(GetUser), new { publicId = user.PublicId }, readDto);
    }

    [HttpPut("{publicId}")]
    public async Task<ActionResult<UserReadDTO>> UpdateUser(Guid publicId, UserWriteDto userWriteDto)
    {
        var user = await _context.Users.FindAsync(publicId);
        if (user is null)
            return NotFound();

        user.UserName = userWriteDto.UserName;
        user.Email = userWriteDto.Email;
        user.PasswordHash = _passwordHasher.HashPassword(user, userWriteDto.Password);

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{publicId}")]
    public async Task<ActionResult> DeleteUser(Guid publicId)
    {
        var user = await _context.Users.Include(u => u.Transactions)
                                       .FirstOrDefaultAsync(u => u.PublicId == publicId); 
        if (user is null)
            return NotFound();

        _context.Transactions.RemoveRange(user.Transactions); 
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}
