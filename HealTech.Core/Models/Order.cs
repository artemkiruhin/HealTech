namespace HealTech.Core.Models;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Created { get; set; } = DateTime.Now;
    public Guid CustomerId { get; set; }
    public decimal TotalPrice { get; set; }

    
    public virtual Customer Customer { get; set; } = null!;
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}