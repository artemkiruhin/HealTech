namespace HealTech.Application.Dto;

public class CustomerDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Role { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime Registered { get; set; }
    public string TaxNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Address { get; set; } = null!;
}