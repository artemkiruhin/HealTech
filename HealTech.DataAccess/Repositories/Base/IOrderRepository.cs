using HealTech.Core.Data;
using HealTech.Core.Models;

namespace HealTech.DataAccess.Repositories.Base;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(Guid customerId);
    Task<IEnumerable<Order>> GetOrdersWithDetailsAsync();
    Task<Order?> GetOrderWithDetailsAsync(Guid orderId);
}