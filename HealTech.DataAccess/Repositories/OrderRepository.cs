using HealTech.Core.Models;
using HealTech.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace HealTech.DataAccess.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(DbContext context) : base(context) { }

    public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(Guid customerId)
    {
        return await _dbSet.Where(o => o.CustomerId == customerId).ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetOrdersWithDetailsAsync()
    {
        return await _dbSet
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .Include(o => o.Customer)
            .ToListAsync();
    }

    public async Task<Order?> GetOrderWithDetailsAsync(Guid orderId)
    {
        return await _dbSet
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .Include(o => o.Customer)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }
}