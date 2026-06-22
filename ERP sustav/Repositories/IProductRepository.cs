using ERP_sustav.Models;

namespace ERP_sustav.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<bool> CategoryExistsAsync(int categoryId);
    Task<bool> SkuExistsAsync(string sku, int? excludedProductId = null);

    Task AddAsync(Product product);
    void Update(Product product);
    void Delete(Product product);
    Task SaveChangesAsync();
}
