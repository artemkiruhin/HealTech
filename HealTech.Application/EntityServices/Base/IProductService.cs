using HealTech.Core.Models;

namespace HealTech.Application.EntityServices.Base;

public interface IProductService
{
    Task Add(string name, int quantity, Guid categoryId);
    Task Update(Guid id, string name, int quantity, Guid categoryId);
    Task<IEnumerable<Product>> GetAll();
    Task<Product?> GetById(Guid id);
}