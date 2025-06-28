using gizmogeo.Domain.Entities;

namespace gizmogeo.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task<Category?> GetByNameAsync(string name);
    Task<Category> CreateAsync(Category category);
    Task DeleteAsync(Category category);
    Task<Category> UpdateAsync(Category category);
}
