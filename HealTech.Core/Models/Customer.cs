namespace HealTech.Core.Models;

public class Customer : User
{
    public Customer()
    {
        Role = nameof(UserRole.Customer);
    }
    
    public DateTime Registered { get; set; } = DateTime.Now;
    public required string TaxNumber { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string Address { get; set; }

    
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}