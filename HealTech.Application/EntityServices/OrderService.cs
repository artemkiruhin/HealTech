using HealTech.Application.EntityServices.Base;
using HealTech.Core.Models;
using HealTech.DataAccess.Repositories.Base;

namespace HealTech.Application.EntityServices;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;

    public OrderService (IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task Add(Guid customerId, Guid productId, int quantity)
    {
        throw new NotImplementedException();
    }

    public async Task Update(Guid id, Guid customerId, Guid productId, int quantity)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Order>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<Order?> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
}