using HealTech.Core.Models;

namespace HealTech.Application.EntityServices.Base;

public interface IProductCategoryService
{
    Task Add(string name);
    Task Update(Guid id, string name);
    Task<IEnumerable<ProductCategory>> GetAll();
    Task<ProductCategory?> GetById(Guid id);
}