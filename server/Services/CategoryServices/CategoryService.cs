using System;
using server.DTOs;
using server.Models;
using server.Repositories.CategoryRepositories.Interfaces;
using server.Services.CategoryServices.Interfaces;

namespace server.Services.CategoryServices;

public class CategoryService: ICategoryService
{
    private readonly ICategoryRepository _repository; 

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CategoryDTO>> GetCategories()
    {
        var Categories = await _repository.GetCategories(); 

        return Categories.Select(c => new CategoryDTO(
            c.Id,
            c.Name,
            c.Description
        )); 
    }

    public async Task<CategoryDTO?> GetCategory(int id)
    {
        var category = await _repository.GetCategory(id);
        if (category is null)
            return null;

        return new CategoryDTO(
            category.Id,
            category.Name,
            category.Description
        ); 
    }

    public async Task<CategoryDTO> CreateCategory(CategoryDTO dto)
    {
        var category = new Category
        {
            Name = dto.Name,
            Description = dto.Description
        };

        await _repository.CreateCategory(category);
        await _repository.SaveChangesAsync();

        return new CategoryDTO(
            category.Id,
            category.Name,
            category.Description
        );
    }

    public async Task<bool> UpdateCategory(int id, CategoryDTO dto)
    {
        var existingCategory = await _repository.GetCategory(id);
        if (existingCategory is null)
            return false;
        
        existingCategory.Name = dto.Name;
        existingCategory.Description = dto.Description;

        await _repository.UpdateCategory(existingCategory);
        await _repository.SaveChangesAsync();

        return true; 
    }

    public async Task<bool> DeleteCategory(int id)
    {
        var category = await _repository.GetCategory(id);
        if (category is null)
            return false;

        await _repository.DeleteCategory(category);
        await _repository.SaveChangesAsync(); 

        return true; 
    }

}
