using HealTech.Core.Data;
using HealTech.Core.Models;

namespace HealTech.DataAccess.Repositories.Base;

public interface IProductCategoryRepository : IRepository<ProductCategory>
{
    Task<ProductCategory?> GetByNameAsync(string name);
    Task<IEnumerable<ProductCategory>> GetCategoriesWithProductsAsync();
}