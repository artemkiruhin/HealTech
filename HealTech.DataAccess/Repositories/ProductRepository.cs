using HealTech.Core.Models;
using HealTech.DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace HealTech.DataAccess.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Product>> GetByCategoryIdAsync(Guid categoryId)
    {
        return await _dbSet.Where(p => p.CategoryId == categoryId).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetAvailableProductsAsync()
    {
        return await _dbSet.Where(p => p.Quantity > 0).ToListAsync();
    }

    public async Task<Product?> GetProductWithCategoryAsync(Guid productId)
    {
        return await _dbSet
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == productId);
    }
}