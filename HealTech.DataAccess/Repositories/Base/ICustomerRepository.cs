using HealTech.Core.Data;
using HealTech.Core.Models;

namespace HealTech.DataAccess.Repositories.Base;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer?> GetByEmailAsync(string email);
    Task<Customer?> GetByTaxNumberAsync(string taxNumber);
    Task<IEnumerable<Customer>> GetCustomersWithOrdersAsync();
}