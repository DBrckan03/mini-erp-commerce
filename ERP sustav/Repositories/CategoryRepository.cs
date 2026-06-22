using ERP_sustav.Data;
using ERP_sustav.Models;
using Microsoft.EntityFrameworkCore;

namespace ERP_sustav.Repositories;

public class CategoryRepository : ICategoryRepository
{
        private readonly AppDbContext _db;
    
        public CategoryRepository(AppDbContext db)
        {
            _db = db;
        }
    
        public async Task<List<Category>> GetAllAsync()
        {
            return await _db.Categories
                .AsNoTracking()
                .ToListAsync();
        }
    
        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _db.Categories.FindAsync(id);
        }

        public async Task<bool> NameExistsAsync(
        string name,
        int? excludedCategoryId = null)
        {
            return await _db.Categories.AnyAsync(c =>
                c.Name == name &&
                (!excludedCategoryId.HasValue || c.Id != excludedCategoryId.Value));
        }

        public async Task<bool> HasProductsAsync(int categoryId)
        {
            return await _db.Products.AnyAsync(p => p.CategoryId == categoryId);
        }

    public async Task AddAsync(Category category)
    {
        await _db.Categories.AddAsync(category);
    }

    public void Update(Category category)
    {
        _db.Categories.Update(category);
    }

    public void Delete(Category category)
    {
        _db.Categories.Remove(category);
    }
    public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

    public Task<bool> NameExists(string name, int? excludedCategoryId = null)
    {
        throw new NotImplementedException();
    }
}
