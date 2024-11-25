using HealTech.Application.EntityServices.Base;
using HealTech.Core.Models;
using HealTech.DataAccess.Repositories.Base;

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
}