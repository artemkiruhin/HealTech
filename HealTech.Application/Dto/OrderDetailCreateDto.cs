namespace HealTech.Application.Dto;

public class OrderDetailCreateDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}