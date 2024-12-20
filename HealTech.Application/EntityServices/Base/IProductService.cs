﻿using HealTech.Core.Models;

namespace HealTech.Application.EntityServices.Base;

public interface IProductService
{
    Task Add(string name, int quantity, decimal price, Guid categoryId);
    Task Update(Guid id, string name, decimal price, int quantity, Guid categoryId);
    Task UpdateQuantityDecrement(Guid id, int quantityToDecrement);
    public Task UpdateQuantityIncrement(Guid id, int quantityToIncrement);
    Task<IEnumerable<Product>> GetAll();
    Task<Product?> GetById(Guid id);
    Task<IEnumerable<Product>> GetByFilter(string? name, int? quantity, string? categoryName, decimal? price);
}