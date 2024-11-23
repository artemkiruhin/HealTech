namespace HealTech.Core.Models;

public abstract class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string FirstName { get; set; }
    public required string Surname { get; set; }
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public bool IsActive { get; set; } = true;
    public required string Role { get; init; }
}