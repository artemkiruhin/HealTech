namespace HealTech.Application.Dto;

public class OrderDto
{
    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    public Guid CustomerId { get; set; }
    public decimal TotalPrice { get; set; }
    public CustomerDto Customer { get; set; } = null!;
    public ICollection<OrderDetailDto> OrderDetails { get; set; } = new List<OrderDetailDto>();
}