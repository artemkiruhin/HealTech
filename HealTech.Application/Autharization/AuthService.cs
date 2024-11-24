using HealTech.Application.Autharization.Base;
using HealTech.Application.Jwt.Base;
using HealTech.Core.Models;
using HealTech.DataAccess.Repositories.Base;

namespace HealTech.Application.Autharization;

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

        if (user is null) throw new KeyNotFoundException("");
        
        var userRole = user.Role switch
        {
            nameof(UserRole.Customer) => UserRole.Customer,
            nameof(UserRole.Employee) => UserRole.Employee,
            _ => throw new ArgumentOutOfRangeException()
        };

        var token = _jwtService.GenerateJwtToken(user.Id, userRole);

        return token;

    }
    
}