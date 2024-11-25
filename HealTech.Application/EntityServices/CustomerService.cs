using HealTech.Application.EntityServices.Base;
using HealTech.Core.Models;
using HealTech.DataAccess.Repositories.Base;

namespace HealTech.Application.EntityServices;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;
    private readonly IAuthRepository _authRepository;

    public CustomerService(ICustomerRepository repository, IAuthRepository authRepository)
    {
        _repository = repository;
        _authRepository = authRepository;
    }

    public async Task Register(string firstname, string surname, string username, string passwordHash, string taxNumber, string email,
        string phone, string address)
    {
        var customer = await _authRepository.GetUserByUsername(username);
        if (customer is not null) throw new KeyNotFoundException("This user is already exist");

        await _repository.AddAsync(new Customer()
        {
            Id = Guid.NewGuid(),
            FirstName = firstname,
            Surname = surname,
            Username = username,
            PasswordHash = passwordHash,
            TaxNumber = taxNumber,
            Address = address,
            Registered = DateTime.UtcNow,
            IsActive = true,
            Email = email,
            Phone = phone,
            Role = nameof(UserRole.Customer)
        });
        await _repository.SaveChangesAsync(); // Добавьте этот вызов
    }

    public async Task Update(Guid id, string firstname, string surname, string username, string passwordHash, string taxNumber, string email,
        string phone, string address, bool isActive = true)
    {
        var customer = await _repository.GetByIdAsync(id) ;
        if (customer is null) throw new KeyNotFoundException("This user does not exist");

        customer.FirstName = firstname;
        customer.Surname = surname;
        customer.Username = username;
        customer.PasswordHash = passwordHash;
        customer.TaxNumber = taxNumber;
        customer.Address = address;
        customer.IsActive = isActive;
        customer.Email = email;
        customer.Phone = phone;
        
        await _repository.UpdateAsync(customer);
        await _repository.SaveChangesAsync(); // Добавьте этот вызов
    }

    public async Task<IEnumerable<Customer>> GetAll()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Customer?> GetById(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }
    
    
}