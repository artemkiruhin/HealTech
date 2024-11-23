﻿namespace HealTech.Core.Models;

public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public int Quantity { get; set; }
    public Guid CategoryId { get; set; }

    
    public virtual ProductCategory Category { get; set; } = null!;
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}