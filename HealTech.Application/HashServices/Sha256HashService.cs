using System.Security.Cryptography;
using System.Text;
using HealTech.Application.HashServices.Base;

namespace HealTech.Application.HashServices;

public class Sha256HashService : IHashService
{
    public string ComputeHash(string message)
    {
        var textBytes = Encoding.UTF8.GetBytes(message);

        using var sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(textBytes);

        return Convert.ToBase64String(hashBytes);
    }
}