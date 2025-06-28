using gizmogeo.Domain.Entities;
using gizmogeo.Domain.Interfaces;
using gizmogeo.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace gizmogeo.Infrastructure.Repositories;

public class RoleRepository(FixGoDbContext dbContext) : IRoleRepository
{
    public async Task<Role> CreateAsync(Role role)
    {
        await dbContext.Roles.AddAsync(role);
        await dbContext.SaveChangesAsync();
        return role;
    }

    public async Task DeleteAsync(Role role)
    {
        dbContext.Roles.Remove(role);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Role?> GetByIdAsync(int id)
    {
        var role = await dbContext.Roles
            .FirstOrDefaultAsync(x => x.Id == id);
        return role;

    }

    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        var allRoles = await dbContext.Roles.Include(u => u.Users).ToListAsync();
        return allRoles;
    }

    public async Task<Role?> GetByNameAsync(string name)
    {
        var role = await dbContext.Roles.FirstOrDefaultAsync(x => x.Name == name);
        return role;
    }

    public async Task<Role> UpdateAsync(Role role)
    {
        dbContext.Roles.Update(role);
        await dbContext.SaveChangesAsync();
        return role;
    }
}
