using gizmogeo.Domain.Entities;
using gizmogeo.Domain.Interfaces;
using gizmogeo.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace gizmogeo.Infrastructure.Repositories;

public class UserRepository(FixGoDbContext dbContext) : IUserRepository
{
    public async Task<User> CreateAsync(User user)
    {
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();

        // Eager load Role after saving
        await dbContext.Entry(user).Reference(u => u.Role).LoadAsync();

        return user;
    }


    public async Task DeleteAsync(User user)
    {
        dbContext.Users.Remove(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var allUsers = await dbContext.Users
            .Include(u => u.ServiceRequests)
            .Include(u => u.AcceptedRequestsAsAdmin) 
            .ToListAsync();

        return allUsers;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        var user = await dbContext.Users
            .Include(r => r.Role)
            .FirstOrDefaultAsync(u => u.Id == id);
        return user;
    }


    public async Task<User?> GetByNameAsync(string name)
    {
        var user = await dbContext.Users
            .Include(r => r.Role)
            .FirstOrDefaultAsync(u => u.Name.ToLower() == name.ToLower());
        return user;
    }

    public async Task<User> UpdateAsync(User user)
    {
        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetByPhoneNumberAsync(string phoneNumber)
    {
        var user = await dbContext.Users
             .Include(r => r.Role)
             .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
        return user;
    }

    public async Task<User?> GetByPendingPhoneNumberAsync(string phoneNumber)
    {
        var user = await dbContext.Users
                   .Include(r => r.Role)
                   .FirstOrDefaultAsync(u => u.PendingPhoneNumber == phoneNumber);
        return user;
    }
}
