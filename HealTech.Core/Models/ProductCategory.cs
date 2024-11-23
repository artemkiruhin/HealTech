namespace HealTech.Core.Models;

public class ProductCategory
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }

    
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}