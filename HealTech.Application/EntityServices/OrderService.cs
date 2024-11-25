using HealTech.Application.EntityServices.Base;
using HealTech.Core.Models;
using HealTech.DataAccess.Repositories;
using HealTech.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace HealTech.Application.EntityServices;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;
    private readonly IProductService _productService;

    public OrderService (IOrderRepository repository, ICustomerRepository customerRepository, IProductRepository productRepository, IProductService productService)
    {
        _repository = repository;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
        _productService = productService;
    }

    public async Task Add(Guid customerId, Guid productId, int quantity)
    {
        var customer = await _customerRepository.GetByIdAsync(customerId);
        if (customer is null) throw new KeyNotFoundException("This customer does not exist");
        
        var product = await _productRepository.GetByIdAsync(productId);
        if (product is null) throw new KeyNotFoundException("This product does not exist");
        
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

        await _productService.UpdateQuantityDecrement(productId, quantity);
        
        await _repository.AddAsync(new Order()
        {
            Id = Guid.NewGuid(),
            CustomerId = customerId,
            ProductId = productId,
            Created = DateTime.UtcNow,
            Quantity = quantity,
            TotalPrice = product.Price * quantity
        });
    }

    public async Task Update(Guid id, Guid customerId, Guid productId, int quantity)
    {
        var order = await _repository.GetByIdAsync(id);
        if (order is null) throw new KeyNotFoundException("This order does not exist");
        
        var customer = await _customerRepository.GetByIdAsync(customerId);
        if (customer is null) throw new KeyNotFoundException("This customer does not exist");
        
        var product = await _productRepository.GetByIdAsync(productId);
        if (product is null) throw new KeyNotFoundException("This product does not exist");
        
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

        var dif = order.Quantity - quantity;
        
        if (dif > 0) await _productService.UpdateQuantityDecrement(productId, quantity);
        else await _productService.UpdateQuantityIncrement(productId, quantity);
        
        order.CustomerId = customerId;
        order.ProductId = productId;
        order.Quantity = quantity;
        order.TotalPrice = product.Price * quantity;
        
        await _repository.UpdateAsync(order);
    }

    public async Task<IEnumerable<Order>> GetAll()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Order?> GetById(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<List<Order>> GetFilteredOrders(DateTime? created, string? customerUsername, decimal? totalPrice, string? productName, int? quantity)
    {
        var query = _repository.GetAll().AsQueryable();

        if (created.HasValue)
        {
            query = query.Where(o => o.Created.Date == created.Value.Date);
        }
        if (!string.IsNullOrEmpty(customerUsername))
        {
            query = query.Where(o => o.Customer.Username.Contains(customerUsername));
        }
        if (totalPrice.HasValue)
        {
            query = query.Where(o => o.TotalPrice == totalPrice.Value);
        }
        if (!string.IsNullOrEmpty(productName))
        {
            query = query.Where(o => o.Product.Name.Contains(productName));
        }
        if (quantity.HasValue)
        {
            query = query.Where(o => o.Quantity == quantity);
        }

        return await query.ToListAsync();
    }
}