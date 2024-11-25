namespace HealTech.Core.Models;

public class Employee : User
{
    public Employee()
    {
        Role = nameof(UserRole.Employee);
    }
    
    public DateTime Hired { get; set; } = DateTime.UtcNow;
    public bool IsAdmin { get; set; }
    public decimal Salary { get; set; }
}