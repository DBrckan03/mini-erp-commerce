using ERP_sustav.Data;
using ERP_sustav.DTOs;
using ERP_sustav.Models;
using ERP_sustav.Repositories;

namespace ERP_sustav.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly AppDbContext _db;

    public CategoryService(
        ICategoryRepository categoryRepository,
        AppDbContext db)
    {
        _categoryRepository = categoryRepository;
        _db = db;
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await _categoryRepository.GetAllAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _categoryRepository.GetByIdAsync(id);
    }

    public async Task<Category> CreateAsync(CreateCategoryDto dto)
    {
        if (await _categoryRepository.NameExistsAsync(dto.Name))
            throw new ArgumentException("Kategorija s tim nazivom već postoji.");

        var category = new Category
        {
            Name = dto.Name,
            Description = dto.Description
        };

        _db.Categories.Add(category);
        await _db.SaveChangesAsync();

        return category;
    }

    public async Task<bool> UpdateAsync(int id, CreateCategoryDto dto)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category is null)
            return false;

        if (await _categoryRepository.NameExistsAsync(dto.Name, id))
            throw new ArgumentException("Kategorija s tim nazivom već postoji.");

        category.Name = dto.Name;
        category.Description = dto.Description;

        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category is null)
            return false;

        if (await _categoryRepository.HasProductsAsync(id))
            throw new ArgumentException(
                "Kategorija se ne može obrisati dok sadrži proizvode.");

        _db.Categories.Remove(category);
        await _db.SaveChangesAsync();

        return true;
    }
}