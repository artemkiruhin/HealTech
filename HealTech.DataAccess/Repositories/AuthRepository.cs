using HealTech.Core.Models;
using HealTech.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace HealTech.DataAccess.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _context;
    private readonly ICustomerRepository _customerRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public AuthRepository(AppDbContext context, ICustomerRepository customerRepository, IEmployeeRepository employeeRepository)
    {
        _context = context;
        _customerRepository = customerRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Username == username);
        if (user is null) return null;

        if (user.Role == nameof(UserRole.Customer)) return await _customerRepository.GetByIdAsync(user.Id);
        return await _employeeRepository.GetByIdAsync(user.Id);
    }
}