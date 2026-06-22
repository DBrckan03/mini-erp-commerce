using ERP_sustav.Models;

namespace ERP_sustav.Repositories;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task<bool> NameExistsAsync(string name, int? excludedCategoryId = null);
    Task<bool> HasProductsAsync(int categoryId);

    Task AddAsync(Category category);
    void Update(Category category);
    void Delete(Category category);
    Task SaveChangesAsync();
}
