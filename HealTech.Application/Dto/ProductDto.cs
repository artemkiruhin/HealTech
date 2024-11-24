namespace HealTech.Application.Dto;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int Quantity { get; set; }
    public Guid CategoryId { get; set; }
    public ProductCategoryDto Category { get; set; } = null!;
}