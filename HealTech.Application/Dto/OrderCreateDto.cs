namespace HealTech.Application.Dto;

public class OrderCreateDto
{
    public Guid CustomerId { get; set; }
    public ICollection<OrderDetailCreateDto> OrderDetails { get; set; } = new List<OrderDetailCreateDto>();
}