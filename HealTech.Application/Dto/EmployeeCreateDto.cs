namespace HealTech.Application.Dto;

public class EmployeeCreateDto
{
    public required string FirstName { get; set; }
    public required string Surname { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public bool IsAdmin { get; set; }
    public decimal Salary { get; set; }
}