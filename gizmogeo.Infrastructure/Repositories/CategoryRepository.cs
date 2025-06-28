using gizmogeo.Domain.Entities;
using gizmogeo.Domain.Interfaces;
using gizmogeo.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace gizmogeo.Infrastructure.Repositories;

public class CategoryRepository(FixGoDbContext dbContext) : ICategoryRepository
{
    public async Task<Category> CreateAsync(Category category)
    {
        dbContext.Categories.Add(category);
        await dbContext.SaveChangesAsync();
        return category;
    }

    public async Task DeleteAsync(Category category)
    {
        dbContext.Categories.Remove(category);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        var allCategories = await dbContext.Categories
            .Include(s => s.ServiceRequests)
            .Include(a => a.AcceptedRequests)
            .ToListAsync();
        return allCategories;
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        var category = await dbContext.Categories
            .Include(s => s.ServiceRequests)
            .Include(a => a.AcceptedRequests)
            .FirstOrDefaultAsync(c => c.Id == id);
        return category;
    }

    public async Task<Category?> GetByNameAsync(string name)
    {
        var category = await dbContext.Categories
            .Include(s => s.ServiceRequests)
            .Include(a => a.AcceptedRequests)
            .FirstOrDefaultAsync(c => c.Name == name);
        return category;
    }

    public async Task<Category> UpdateAsync(Category category)
    {
        dbContext.Categories.Update(category);
        await dbContext.SaveChangesAsync();
        return category;
    }
}
