using HealTech.Core.Models;

namespace HealTech.Application.EntityServices.Base;

public interface IEmployeeService
{
    Task Register(string firstname, string surname, string username, string passwordHash, decimal salary, bool isAdmin = false, bool isActive = true);
    Task Update(Guid id, string firstname, string surname, string passwordHash, decimal salary, bool isAdmin = false, bool isActive = true);
    Task<IEnumerable<Employee>> GetAll();
    Task<Employee?> GetById(Guid id);
}