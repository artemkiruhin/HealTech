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

    public async Task Update(Guid orderId, Guid customerId, Guid productId, int quantity)
    {
        // Validate input
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity, nameof(quantity));

        // Retrieve the order
        var order = await _repository.GetByIdAsync(orderId)
            ?? throw new Exception($"Order with ID {orderId} not found.");

        // Validate customer
        var customer = await _customerRepository.GetByIdAsync(customerId)
            ?? throw new Exception($"Customer with ID {customerId} not found.");

        // Retrieve the product
        var product = await _productRepository.GetByIdAsync(productId)
            ?? throw new Exception($"Product with ID {productId} not found.");

        // Calculate quantity difference
        var quantityDifference = quantity - order.Quantity;

        // Update product quantity based on difference
        if (quantityDifference > 0)
        {
            // Need to check if sufficient product is available
            if (quantityDifference > product.Quantity)
                throw new InvalidOperationException("Insufficient product quantity for order update.");

            await _productService.UpdateQuantityDecrement(productId, quantityDifference);
        }
        else if (quantityDifference < 0)
        {
            await _productService.UpdateQuantityIncrement(productId, Math.Abs(quantityDifference));
        }

        // Update order details
        order.CustomerId = customerId;
        order.ProductId = productId;
        order.Quantity = quantity;
        order.TotalPrice = product.Price * quantity;

        // Save changes
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
        var query = _repository.GetAll();

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