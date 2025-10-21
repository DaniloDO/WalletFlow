using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.DTOs;
using server.Models;
using server.Services.CategoryServices.Interfaces;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    // private readonly AppDbContext _context;
    private readonly ICategoryService _service;
    // public CategoriesController(AppDbContext context)
    // {
    //     _context = context;
    // }

    public CategoriesController(ICategoryService service)
    {
        _service = service; 
    }

    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
    // {
    //     var categories = await _context.Categories
    //                                    .Select(c => new CategoryDTO(c.Id, c.Name, c.Description))
    //                                    .ToListAsync();

    //     return Ok(categories); 
    // }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
    {
        var categories = await _service.GetCategories();
        return Ok(categories);
    }

    // [HttpGet("{id}")]
    // public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
    // {
    //     var category = await _context.Categories.Where(c => c.Id == id)
    //                                             .Select(c => new CategoryDTO(c.Id, c.Name, c.Description))
    //                                             .FirstOrDefaultAsync(); 

    //     if (category is null)
    //         return NotFound();
        
    //     return Ok(category); 
    // }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
    {
        var category = await _service.GetCategory(id);
        if (category is null)
            return NotFound();

        return Ok(category); 

    }

    // [HttpPost]
    // public async Task<ActionResult<CategoryDTO>> CreateCategory(CategoryDTO dto)
    // {
    //     var category = new Category { Name = dto.Name, Description = dto.Description }; 
    //     _context.Categories.Add(category);
    //     await _context.SaveChangesAsync();

    //     var resultDto = new CategoryDTO(category.Id, category.Name, category.Description);
    //     return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, resultDto);  
    // }

    [HttpPost]
    public async Task<ActionResult<CategoryDTO>> CreateCategory(CategoryDTO dto)
    {
        var createdCategory = await _service.CreateCategory(dto);

        return CreatedAtAction(nameof(GetCategory), new {id = createdCategory.Id} ,createdCategory);  
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateCategory(int id, CategoryDTO dto)
    {
        var updated = await _service.UpdateCategory(id, dto); 
        if (!updated)
            return NotFound();

        return NoContent();  
    }

    // [HttpDelete("{id}")]
    // public async Task<ActionResult> DeleteCategory(int id)
    // {
    //     var category = await _context.Categories.FindAsync(id);
    //     if (category is null)
    //         return NotFound();

    //     _context.Categories.Remove(category);
    //     await _context.SaveChangesAsync();

    //     return NoContent();
    // }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        var deleted = await _service.DeleteCategory(id);
        if (!deleted)
            return NotFound();

        return NoContent(); 
    }
}
