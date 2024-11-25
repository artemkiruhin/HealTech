namespace HealTech.Application.EntityServices.Base;

public interface IAuthService
{
    Task<string> Login(string username, string passwordHash);
}