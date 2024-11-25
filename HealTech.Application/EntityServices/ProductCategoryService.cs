using HealTech.Application.EntityServices.Base;
using HealTech.Core.Models;
using HealTech.DataAccess.Repositories.Base;

namespace HealTech.Application.EntityServices;

public class ProductCategoryService : IProductCategoryService
{
    private readonly IProductCategoryRepository _repository;

    
    private async Task<bool> IsCategoryExists(Guid categoryId)
    {
        var category = await _repository.GetByIdAsync(categoryId);
        return category is not null;
    }
    private async Task<bool> IsCategoryExists(string name)
    {
        var category = await _repository.GetByNameAsync(name);
        return category is not null;
    }
    
    public ProductCategoryService(IProductCategoryRepository repository)
    {
        _repository = repository;
    }
    
    public async Task Add(string name)
    {
        if (await IsCategoryExists(name)) throw new ArgumentException("This category is already exists");
        await _repository.AddAsync(new ProductCategory()
        {
            Id = Guid.NewGuid(),
            Name = name
        });
    }

    public async Task Update(Guid id, string name)
    {
        if (await IsCategoryExists(name)) throw new ArgumentException("This category is already exists");

        var category = await _repository.GetByIdAsync(id);
        if (category is null) throw new ArgumentException("This category does not exist");
        category.Name = name;

        await _repository.UpdateAsync(category);
    }

    public async Task<IEnumerable<ProductCategory>> GetAll()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<ProductCategory?> GetById(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }
}