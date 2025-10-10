using Microsoft.AspNetCore.Mvc;
using server.DTOs;
using server.Services.UserServices.Interfaces;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service; 

    public UsersController(IUserService service)
    {
        _service = service; 
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SimpleUserReadDTO>>> GetUsers()
    {
        var users = await _service.GetUsers(); 
        return Ok(users); 
    }

    [HttpGet("{publicId:guid}")]
    public async Task<ActionResult<UserReadDTO>> GetUser(Guid publicId)
    {
        var user = await _service.GetUser(publicId);
        if(user is null)
            return NotFound();

        return Ok(user); 

    }

    [HttpPost]
    public async Task<ActionResult<SimpleUserReadDTO>> CreateUser(UserWriteDTO dto)
    {
        var createdUser = await _service.CreateUser(dto);

        return CreatedAtAction(nameof(GetUser), new { publicId = createdUser.PublicId }, createdUser);  
    }

    [HttpPut("{publicId:guid}")]
    public async Task<ActionResult> UpdateUser(Guid publicId, UserWriteDTO dto)
    {
        var success = await _service.UpdateUser(publicId, dto);
        if(!success)
            return NotFound();  

        return NoContent(); 
    }

    [HttpDelete("{publicId:guid}")]
    public async Task<ActionResult> DeleteUser(Guid publicId)
    {
        var success = await _service.DeleteUser(publicId); 
        if(!success)
            return NotFound();

        return NoContent(); 
    }

}
