using HealTech.Core.Models;

namespace HealTech.DataAccess.Repositories.Base;

public interface IAuthRepository
{
    Task<User?> GetUserByUsername(string username);
}