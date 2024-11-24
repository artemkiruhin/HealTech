using System.Security.Claims;
using HealTech.Core.Models;

namespace HealTech.Application.Jwt.Base;

public interface IJwtService
{
    string GenerateJwtToken(Guid id, UserRole role);
    ClaimsPrincipal? ValidateToken(string token);
    (Guid? Id, UserRole? Role) GetIdAndRoleFromClaims(ClaimsPrincipal principal);
}