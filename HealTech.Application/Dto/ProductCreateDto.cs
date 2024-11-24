namespace HealTech.Application.Dto;

public class ProductCreateDto
{
    public required string Name { get; set; }
    public int Quantity { get; set; }
    public Guid CategoryId { get; set; }
}