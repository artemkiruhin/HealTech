namespace HealTech.Application.Dto;

public class CustomerCreateDto
{
    public required string FirstName { get; set; }
    public required string Surname { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string TaxNumber { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string Address { get; set; }
}