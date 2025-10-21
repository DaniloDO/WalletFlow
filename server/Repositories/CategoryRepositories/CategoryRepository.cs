using System;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using server.Repositories.CategoryRepositories.Interfaces;

namespace server.Repositories.CategoryRepositories;

public class CategoryRepository : ICategoryRepository
{
    public readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Category>> GetCategories()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category?> GetCategory(int id)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task CreateCategory(Category category)
    {
        _context.Categories.Add(category);
    }

    public async Task UpdateCategory(Category category)
    {
        _context.Categories.Update(category);
    }

    public async Task DeleteCategory(Category category)
    {
        _context.Categories.Remove(category);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}
