using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using HealTech.Application.Jwt.Base;
using HealTech.Core.Models;
using Microsoft.IdentityModel.Tokens;

namespace HealTech.Application.Jwt;

public class JwtService : IJwtService
{
    private readonly string _key;
    private readonly int _expires;
    private readonly string _audience;
    private readonly string _issuer;

    public JwtService(JwtSettings jwtSettings)
    {
        _key = jwtSettings.SecretKey;
        _expires = jwtSettings.Expires;
        _audience = jwtSettings.Audience;
        _issuer = jwtSettings.Issuer;
    }

    public string GenerateJwtToken(Guid id, UserRole role)
    {
        var claims = new Claim[]
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, id.ToString()),
            new("role",  role  == UserRole.Customer ? nameof(UserRole.Customer) : nameof(UserRole.Employee))
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            audience: _audience,
            expires: DateTime.Now.AddHours(_expires),
            issuer: _issuer,
            claims: claims,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal? ValidateToken(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return null;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_key);

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _issuer,
            ValidAudience = _audience,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero
        };

        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

            if (validatedToken is JwtSecurityToken jwtToken && jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return principal;
            }
        }
        catch (Exception)
        {
            
        }

        return null;
    }
    
    public (Guid? Id, UserRole? Role) GetIdAndRoleFromClaims(ClaimsPrincipal principal)
    {
        if (principal == null)
        {
            return (null, null);
        }
        
        var idClaim = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        Guid? id = idClaim != null ? Guid.Parse(idClaim) : null;
        
        var role = principal.FindFirst("role")?.Value;

        UserRole? userRole = null;
        
        if (role is not null)
        {
            userRole = role switch
            {
                nameof(UserRole.Customer) => UserRole.Customer,
                nameof(UserRole.Employee) => UserRole.Employee
            };
        }
        
        
        return (id, userRole);
    }
}