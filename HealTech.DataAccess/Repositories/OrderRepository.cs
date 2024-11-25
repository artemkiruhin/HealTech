﻿using HealTech.Core.Models;
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
}