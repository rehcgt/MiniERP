using MiniERP.Application.Interfaces;
using MiniERP.Domain.Entities;

namespace MiniERP.Application.Services;

public class CategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(
        ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<(bool Success, string Message)> CreateAsync(string name)
    {
        if (await _repository.ExistsNameAsync(name))
        {
            return (false, "A category with this name already exists.");
        }

        var category = new Category
        {
            Name = name
        };

        await _repository.AddAsync(category);
        await _repository.SaveChangesAsync();

        return (true, "Category created successfully.");
    }

    public async Task<(bool Success, string Message)> UpdateAsync(int id, string name)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category == null)
        {
            return (false, "Category not found.");
        }

        if (await _repository.ExistsNameAsync(name, id))
        {
            return (false, "Another category with this name already exists.");
        }

        category.Name = name;

        await _repository.UpdateAsync(category);
        await _repository.SaveChangesAsync();

        return (true, "Category updated successfully.");
    }

    public async Task<(bool Success, string Message)> DeleteAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category == null)
        {
            return (false, "Category not found.");
        }

        await _repository.DeleteAsync(id);
        await _repository.SaveChangesAsync();

        return (true, "Category deleted successfully.");
    }
}