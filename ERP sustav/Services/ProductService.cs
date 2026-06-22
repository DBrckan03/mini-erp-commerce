using ERP_sustav.DTOs;
using ERP_sustav.Models;
using ERP_sustav.Repositories;

namespace ERP_sustav.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _productRepository.GetByIdAsync(id);
    }

    public async Task<Product> CreateAsync(CreateProductDto dto)
    {
        if (!await _productRepository.CategoryExistsAsync(dto.CategoryId))
            throw new ArgumentException("Odabrana kategorija ne postoji.");

        if (await _productRepository.SkuExistsAsync(dto.Sku))
            throw new ArgumentException("Proizvod s tim SKU-om već postoji.");

        var product = new Product
        {
            Name = dto.Name,
            Sku = dto.Sku,
            Description = dto.Description,
            Price = dto.Price,
            VarRate = dto.VarRate,
            StockQuantity = dto.StockQuantity,
            CategoryId = dto.CategoryId
        };

        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();

        return product;
    }

    public async Task<bool> UpdateAsync(int id, CreateProductDto dto)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
            return false;

        if (!await _productRepository.CategoryExistsAsync(dto.CategoryId))
            throw new ArgumentException("Odabrana kategorija ne postoji.");

        if (await _productRepository.SkuExistsAsync(dto.Sku, id))
            throw new ArgumentException("Proizvod s tim SKU-om već postoji.");

        product.Name = dto.Name;
        product.Sku = dto.Sku;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.VarRate = dto.VarRate;
        product.StockQuantity = dto.StockQuantity;
        product.CategoryId = dto.CategoryId;

        _productRepository.Update(product);
        await _productRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
            return false;

        _productRepository.Delete(product);
        await _productRepository.SaveChangesAsync();

        return true;
    }
}
