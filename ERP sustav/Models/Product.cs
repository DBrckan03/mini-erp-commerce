namespace ERP_sustav.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Sku { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public decimal VarRate { get; set; }
    public int StockQuantity { get; set; }
    public bool isActive { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}
