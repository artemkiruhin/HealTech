using HealTech.Core.Models;
using HealTech.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace HealTech.DataAccess.Repositories;

public class OrderDetailRepository : BaseRepository<OrderDetail>, IOrderDetailRepository
{
    public OrderDetailRepository(DbContext context) : base(context) { }

    public async Task<IEnumerable<OrderDetail>> GetByOrderIdAsync(Guid orderId)
    {
        return await _dbSet
            .Include(od => od.Product)
            .Where(od => od.OrderId == orderId)
            .ToListAsync();
    }

    public async Task<IEnumerable<OrderDetail>> GetByProductIdAsync(Guid productId)
    {
        return await _dbSet
            .Include(od => od.Order)
            .Where(od => od.ProductId == productId)
            .ToListAsync();
    }
}