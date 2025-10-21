using System;
using server.Models;

namespace server.Repositories.CategoryRepositories.Interfaces;

public interface ICategoryRepository
{
    public Task<IEnumerable<Category>> GetCategories(); 
    public Task<Category?> GetCategory(int Id); 
    public Task CreateCategory(Category category);
    public Task UpdateCategory(Category category); 
    public Task DeleteCategory(Category category); 
    public Task SaveChangesAsync(); 
}
