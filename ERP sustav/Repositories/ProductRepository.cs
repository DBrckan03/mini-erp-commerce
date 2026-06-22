using ERP_sustav.Data;
using ERP_sustav.Models;
using Microsoft.EntityFrameworkCore;

namespace ERP_sustav.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _db;

    public ProductRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _db.Products
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _db.Products.FindAsync(id);
    }

    public async Task<bool> CategoryExistsAsync(int categoryId)
    {
        return await _db.Categories.AnyAsync(c => c.Id == categoryId);
    }

    public async Task<bool> SkuExistsAsync(
        string sku,
        int? excludedProductId = null)
    {
        return await _db.Products.AnyAsync(p =>
            p.Sku == sku &&
            (!excludedProductId.HasValue || p.Id != excludedProductId.Value));
    }

    public async Task AddAsync(Product product)
    {
        await _db.Products.AddAsync(product);
    }

    public void Update(Product product)
    {
        _db.Products.Update(product);
    }

    public void Delete(Product product)
    {
        _db.Products.Remove(product);
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}
