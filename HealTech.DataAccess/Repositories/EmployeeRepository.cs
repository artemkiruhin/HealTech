using HealTech.Core.Models;
using HealTech.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace HealTech.DataAccess.Repositories;

public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Employee>> GetAdminEmployeesAsync()
    {
        return await _dbSet.Where(e => e.IsAdmin).ToListAsync();
    }

    public async Task<IEnumerable<Employee>> GetActiveEmployeesAsync()
    {
        return await _dbSet.Where(e => e.IsActive).ToListAsync();
    }

    public IEnumerable<Employee> GetAll() => _dbSet.ToList();   
}