using HealTech.Application.EntityServices.Base;
using HealTech.Core.Models;
using HealTech.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace HealTech.Application.EntityServices;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IProductCategoryRepository _categoryRepository;

    
    private async Task<bool> IsCategoryExists(Guid categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        return category is not null;
    }
    
    public ProductService(IProductRepository repository, IProductCategoryRepository categoryRepository)
    {
        _repository = repository;
        _categoryRepository = categoryRepository;
    }

    public async Task Add(string name, int quantity, decimal price, Guid categoryId)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(quantity);
        if (!await IsCategoryExists(categoryId)) throw new KeyNotFoundException("This category does not exist");
        await _repository.AddAsync(new Product()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Quantity = quantity,
            CategoryId = categoryId,
            Price = price
        });
    }

    public async Task Update(Guid id, string name, decimal price, int quantity, Guid categoryId)
    {
        // Validate input parameters
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        ArgumentOutOfRangeException.ThrowIfNegative(price, nameof(price));
        ArgumentOutOfRangeException.ThrowIfNegative(quantity, nameof(quantity));

        // Check if category exists
        var category = await _repository.GetByCategoryIdAsync(categoryId);
        if (category == null)
            throw new Exception($"Category with ID {categoryId} not found.");

        // Retrieve the existing product
        var product = await _repository.GetByIdAsync(id)
            ?? throw new Exception($"Product with ID {id} not found.");

        // Update product properties
        product.Name = name;
        product.Price = price;
        product.Quantity = quantity;
        product.CategoryId = categoryId;

        // Save changes
        await _repository.UpdateAsync(product);
    }

    public async Task UpdateQuantityDecrement(Guid productId, int quantityToDecrement)
    {
        // Validate input
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantityToDecrement, nameof(quantityToDecrement));

        // Retrieve the product
        var product = await _repository.GetByIdAsync(productId)
            ?? throw new Exception($"Product with ID {productId} not found.");

        // Ensure sufficient quantity
        if (quantityToDecrement > product.Quantity)
            throw new InvalidOperationException("Insufficient product quantity for decrement.");

        // Decrement quantity
        product.Quantity -= quantityToDecrement;

        // Save changes
        await _repository.UpdateAsync(product);
    }

    public async Task UpdateQuantityIncrement(Guid productId, int quantityToIncrement)
    {
        // Validate input
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantityToIncrement, nameof(quantityToIncrement));

        // Retrieve the product
        var product = await _repository.GetByIdAsync(productId)
            ?? throw new Exception($"Product with ID {productId} not found.");

        // Increment quantity
        product.Quantity += quantityToIncrement;

        // Save changes
        await _repository.UpdateAsync(product);
    }


    public async Task<IEnumerable<Product>> GetByFilter(string? name, int? quantity, string? categoryName, decimal? price)
    {
        var query = _repository.GetAll();

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(p => p.Name.Contains(name));
        }
        if (quantity.HasValue)
        {
            query = query.Where(p => p.Quantity == quantity.Value);
        }
        if ((!string.IsNullOrEmpty(categoryName)))
        {
            query = query.Where(p => p.Category.Name.Contains(categoryName));
        }
        if (price.HasValue)
        {
            query = query.Where(p => p.Price == price.Value);
        }
        
        return await query.ToListAsync();
    } 

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Product?> GetById(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }
}