using HealTech.Core.Data;
using HealTech.Core.Models;

namespace HealTech.DataAccess.Repositories.Base;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetByCategoryIdAsync(Guid categoryId);
    Task<IEnumerable<Product>> GetAvailableProductsAsync();
    Task<Product?> GetProductWithCategoryAsync(Guid productId);
    IEnumerable<Product> GetAll();
}