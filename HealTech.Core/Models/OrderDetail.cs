namespace HealTech.Core.Models;

public class OrderDetail
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    
    public virtual Order Order { get; set; } = null!;
    public virtual Product Product { get; set; } = null!;
}