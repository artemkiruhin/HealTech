﻿using HealTech.Application.EntityServices.Base;
using HealTech.Core.Models;
using HealTech.DataAccess.Repositories.Base;

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
        ArgumentOutOfRangeException.ThrowIfNegative(quantity);
        if (!await IsCategoryExists(categoryId)) throw new KeyNotFoundException("This category does not exist");
        var product = await _repository.GetByIdAsync(id);

        if (product is null) throw new KeyNotFoundException("This product does not exist");
        
        product.Name = name;
        product.Quantity = quantity;
        product.CategoryId = categoryId;
        product.Price = price;

        await _repository.UpdateAsync(product);
    }

    public async Task UpdateQuantityDecrement(Guid id, int quantityToDecrement)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(quantityToDecrement);

        var product = await _repository.GetByIdAsync(id);
        if (product is null) throw new KeyNotFoundException("This product does not exist");

        ArgumentOutOfRangeException.ThrowIfGreaterThan(quantityToDecrement, product.Quantity);

        product.Quantity -= quantityToDecrement;

        await _repository.UpdateAsync(product);
    }
    public async Task UpdateQuantityIncrement(Guid id, int quantityToIncrement)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(quantityToIncrement);

        var product = await _repository.GetByIdAsync(id);
        if (product is null) throw new KeyNotFoundException("This product does not exist");

        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(quantityToIncrement, product.Quantity);

        product.Quantity += quantityToIncrement;

        await _repository.UpdateAsync(product);
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