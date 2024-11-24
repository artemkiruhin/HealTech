namespace HealTech.Application.Dto;

public class ProductCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<ProductDto> Products { get; set; } = new List<ProductDto>();
}