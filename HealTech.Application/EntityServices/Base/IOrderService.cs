using HealTech.Application.Dto;
using HealTech.Core.Models;

namespace HealTech.Application.EntityServices.Base;

public interface IOrderService
{
    Task Add(Guid customerId, Guid productId, int quantity);
    Task Update(Guid id, Guid customerId, Guid productId, int quantity);
    Task<IEnumerable<Order>> GetAll();
    Task<Order?> GetById(Guid id);
}