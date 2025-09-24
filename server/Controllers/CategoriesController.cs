using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.DTos;
using server.Models;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly AppDbContext _context;
    public CategoriesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
    {
        var categories = await _context.Categories
                                       .Select(c => new CategoryDTO(c.Id, c.Name, c.Description))
                                       .ToListAsync();

        return Ok(categories); 
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
    {
        var category = await _context.Categories.Where(c => c.Id == id)
                                                .Select(c => new CategoryDTO(c.Id, c.Name, c.Description))
                                                .FirstOrDefaultAsync(); 

        if (category is null)
            return NotFound();
        
        return Ok(category); 
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDTO>> CreateCategory(CategoryDTO dto)
    {
        var category = new Category { Name = dto.Name, Description = dto.Description }; 
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        var resultDto = new CategoryDTO(category.Id, category.Name, category.Description);
        return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, resultDto);  
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category is null)
            return NotFound();

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
