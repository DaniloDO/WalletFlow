using System;
using server.DTOs;

namespace server.Services.CategoryServices.Interfaces;

public interface ICategoryService
{
    public Task<IEnumerable<CategoryDTO>> GetCategories();
    public Task<CategoryDTO?> GetCategory(int Id);
    public Task<CategoryDTO> CreateCategory(CategoryDTO dto); 
    public Task<bool> UpdateCategory(int Id, CategoryDTO dto);
    public Task<bool> DeleteCategory(int Id); 
}
