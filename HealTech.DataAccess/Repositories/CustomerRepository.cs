using HealTech.Core.Models;
using HealTech.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace HealTech.DataAccess.Repositories;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(AppDbContext context) : base(context) { }

    public async Task<Customer?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.Email == email);
    }

    public async Task<Customer?> GetByTaxNumberAsync(string taxNumber)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.TaxNumber == taxNumber);
    }

    public async Task<IEnumerable<Customer>> GetCustomersWithOrdersAsync()
    {
        return await _dbSet.Include(c => c.Orders).ToListAsync();
    }

    public IQueryable<Customer> GetAll()
    {
        return _dbSet.AsQueryable();
    }
}