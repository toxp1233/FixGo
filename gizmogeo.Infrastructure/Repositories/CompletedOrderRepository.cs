using gizmogeo.Domain.Entities;
using gizmogeo.Domain.Interfaces;
using gizmogeo.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace gizmogeo.Infrastructure.Repositories;

public class CompletedOrderRepository(FixGoDbContext dbContext) : ICompletedOrderRepository
{
    public async Task<CompletedOrder> CreateAsync(CompletedOrder record)
    {
        await dbContext.CompletedOrders.AddAsync(record);
        await dbContext.SaveChangesAsync();
        return record;
    }

    public async Task<IEnumerable<CompletedOrder>> GetAllAsync()
    {
        var allCompletedOrders = await dbContext.CompletedOrders
            .Include(c => c.CompletedByUser)
            .Include(c => c.AcceptedRequest)
            .ThenInclude(ar => ar.ServiceRequest)
            .ThenInclude(sr => sr.User)
            .ToListAsync();

        return allCompletedOrders;
    }

    public async Task<IEnumerable<CompletedOrder?>> GetByAdmin(Guid adminId)
    {
        var allCompletedOrders = await dbContext.CompletedOrders
            .Where(a => a.CompletedByUserId == adminId)
            .Include(u => u.CompletedByUser)
            .Include(c => c.AcceptedRequest)
            .ThenInclude(ar => ar.ServiceRequest)
            .ThenInclude(sr => sr.User)
            .ToListAsync();

        return allCompletedOrders;
    }

    public async Task<CompletedOrder?> GetByIdAsync(Guid id)
    {
        var completedOrder = await dbContext.CompletedOrders
            .Include(u => u.CompletedByUser)
            .Include(c => c.AcceptedRequest)
            .ThenInclude(ar => ar.ServiceRequest)
            .ThenInclude(sr => sr.User)
            .FirstOrDefaultAsync(c => c.Id == id);
        return completedOrder;
    }

    public async Task<CompletedOrder> UpdateAsync(CompletedOrder record)
    {
        dbContext.CompletedOrders.Update(record);
        await dbContext.SaveChangesAsync();
        return record;
    }
}
