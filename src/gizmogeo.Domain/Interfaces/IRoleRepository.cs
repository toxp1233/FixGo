using gizmogeo.Domain.Entities;

namespace gizmogeo.Domain.Interfaces;

public interface IRoleRepository
{
    Task<Role> CreateAsync(Role role);
    Task<Role> UpdateAsync(Role role);
    Task DeleteAsync(Role role);
    Task<Role?> GetByIdAsync(int id);
    Task<Role?> GetByNameAsync(string name);
    Task<IEnumerable<Role>> GetAllAsync();
}
