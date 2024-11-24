namespace HealTech.Application.Autharization.Base;

public interface IAuthService
{
    Task<string> Login(string username, string passwordHash);
}