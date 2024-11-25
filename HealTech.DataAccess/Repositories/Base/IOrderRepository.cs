using HealTech.Core.Data;
using HealTech.Core.Models;

namespace HealTech.DataAccess.Repositories.Base;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(Guid customerId);
    IEnumerable<Order> GetAll();
}