namespace HealTech.Core.Models;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Created { get; set; } = DateTime.Now;
    public Guid CustomerId { get; set; }
    public decimal TotalPrice { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public virtual Product Product { get; set; } = null!;
    public virtual Customer Customer { get; set; } = null!;
    
}