namespace HealTech.Application.Dto;

public class EmployeeDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Role { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime Hired { get; set; }
    public bool IsAdmin { get; set; }
    public decimal Salary { get; set; }
}