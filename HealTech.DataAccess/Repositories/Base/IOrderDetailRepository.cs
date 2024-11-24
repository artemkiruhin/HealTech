using HealTech.Core.Data;
using HealTech.Core.Models;

namespace HealTech.DataAccess.Repositories.Base;

public interface IOrderDetailRepository : IRepository<OrderDetail>
{
    Task<IEnumerable<OrderDetail>> GetByOrderIdAsync(Guid orderId);
    Task<IEnumerable<OrderDetail>> GetByProductIdAsync(Guid productId);
}