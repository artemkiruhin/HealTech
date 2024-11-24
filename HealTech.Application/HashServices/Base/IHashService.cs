namespace HealTech.Application.HashServices.Base;

public interface IHashService
{
    string ComputeHash(string message);
}