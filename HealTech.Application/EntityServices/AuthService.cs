using HealTech.Application.EntityServices.Base;
using HealTech.Application.Jwt.Base;
using HealTech.Core.Models;
using HealTech.DataAccess.Repositories.Base;

namespace HealTech.Application.EntityServices;

public class AuthService : IAuthService
{
    private readonly IJwtService _jwtService;
    private readonly IAuthRepository _authRepository;

    public AuthService (IJwtService jwtService, IAuthRepository authRepository)
    {
        _jwtService = jwtService;
        _authRepository = authRepository;
    }
    
    public async Task<string> Login(string username, string passwordHash)
    {
        var user = await _authRepository.GetUserByUsername(username);
        if (user is null) throw new KeyNotFoundException("This user is not exist");
        if (user.PasswordHash != passwordHash) throw new KeyNotFoundException("Invalid username or password");

        var token = user.Role switch
        {
            nameof(UserRole.Employee) => _jwtService.GenerateJwtToken(user.Id, UserRole.Employee),
            nameof(UserRole.Customer) => _jwtService.GenerateJwtToken(user.Id, UserRole.Customer),
            _ => throw new KeyNotFoundException("Invalid username or password")
        };

        return token;
    }
}