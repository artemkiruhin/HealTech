using HealTech.Core.Models;
using HealTech.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace HealTech.DataAccess.Repositories;

public class ProductCategoryRepository : BaseRepository<ProductCategory>, IProductCategoryRepository
{
    public ProductCategoryRepository(DbContext context) : base(context) { }

    public async Task<ProductCategory?> GetByNameAsync(string name)
    {
        return await _dbSet.FirstOrDefaultAsync(pc => pc.Name == name);
    }

    public async Task<IEnumerable<ProductCategory>> GetCategoriesWithProductsAsync()
    {
        return await _dbSet.Include(pc => pc.Products).ToListAsync();
    }
}