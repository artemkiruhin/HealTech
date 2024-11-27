using HealTech.Core.Data;
using HealTech.Core.Models;

namespace HealTech.DataAccess.Repositories.Base;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<IEnumerable<Employee>> GetAdminEmployeesAsync();
    Task<IEnumerable<Employee>> GetActiveEmployeesAsync();
    IQueryable<Employee> GetAll();
}