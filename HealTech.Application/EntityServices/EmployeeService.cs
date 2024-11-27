using HealTech.Application.EntityServices.Base;
using HealTech.Core.Models;
using HealTech.DataAccess.Repositories;
using HealTech.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace HealTech.Application.EntityServices;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;
    private readonly IAuthRepository _authRepository;

    public EmployeeService (IEmployeeRepository repository, IAuthRepository authRepository)
    {
        _repository = repository;
        _authRepository = authRepository;
    }

    public async Task Register(string firstname, string surname, string username, string passwordHash, decimal salary,
        bool isAdmin = false, bool isActive = true)
    {
        var employee = await _authRepository.GetUserByUsername(username);
        if (employee is not null) throw new KeyNotFoundException("This user is already exist");

        await _repository.AddAsync(new Employee()
        {
            Id = Guid.NewGuid(),
            FirstName = firstname,
            Surname = surname,
            Username = username,
            PasswordHash = passwordHash,
            Salary = salary,
            IsAdmin = isAdmin,
            IsActive = isActive,
            Role = nameof(UserRole.Employee),
            Hired = DateTime.UtcNow
        });
    }

    public async Task Update(Guid id, string firstname, string surname, string passwordHash, decimal salary,
        bool isAdmin = false, bool isActive = true)
    {
        var employee = await _repository.GetByIdAsync(id);
        if (employee is null) throw new KeyNotFoundException("This user does not exist");

        employee.FirstName = firstname;
        employee.Surname = surname;
        employee.PasswordHash = passwordHash;
        employee.Salary = salary;
        employee.IsAdmin = isAdmin;
        employee.IsActive = isActive;
        
        await _repository.UpdateAsync(employee);
    }

    public async Task<IEnumerable<Employee>> GetAll()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Employee?> GetById(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Employee>> GetByFilter(string? username, DateTime? hired, bool? isAdmin, decimal? salary, string? firstname, string? surname, bool? isActive)
    {
        var query = _repository.GetAll().AsQueryable();

        if (!string.IsNullOrEmpty(username))
        {
            query = query.Where(x => x.Username.Contains(username));
        }
        if (!string.IsNullOrEmpty(firstname))
        {
            query = query.Where(x => x.FirstName.Contains(firstname));
        }
        if (!string.IsNullOrEmpty(surname))
        {
            query = query.Where(x => x.Surname.Contains(surname));
        }
        if (hired.HasValue)
        {
            query = query.Where(x => x.Hired.Date == hired.Value.Date);
        }
        if (isAdmin.HasValue)
        {
            query = query.Where(x => x.IsAdmin == isAdmin.Value);
        }
        if (isActive.HasValue)
        {
            query = query.Where(x => x.IsActive == isActive.Value);
        }
        if (salary.HasValue)
        {
            query = query.Where(x => x.Salary == salary.Value);
        }

        return await query.ToListAsync();
    }
}