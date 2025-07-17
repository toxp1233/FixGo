using gizmogeo.Domain.Entities;
using gizmogeo.Domain.Interfaces;
using gizmogeo.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace gizmogeo.Infrastructure.Repositories;

public class AcceptedRequestsRepository(FixGoDbContext dbContext) : IAcceptedRequestsRepository
{
    public async Task<AcceptedRequest> CreateAsync(AcceptedRequest acceptedRequest)
    {
        await dbContext.AcceptedRequests.AddAsync(acceptedRequest);
        await dbContext.SaveChangesAsync();
        return acceptedRequest;
    }

    public async Task DeleteAsync(AcceptedRequest acceptedRequest)
    {
        dbContext.AcceptedRequests.Remove(acceptedRequest);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<AcceptedRequest>> GetAllAsync()
    {
        var allAcceptedRequests = await dbContext.AcceptedRequests
            .Include(c => c.Category)
            .Include(u => u.User)
            .Include(r => r.ServiceRequest)
            .ToListAsync();

        return allAcceptedRequests;
    }

    public async Task<IEnumerable<AcceptedRequest>> GetByUserIdAsync(Guid userId)
    {
        var acceptedRequest = await dbContext.AcceptedRequests.Where(a => a.UserId == userId)
            .Include(c => c.Category)
            .Include(u => u.User)
            .Include(r => r.ServiceRequest)
            .ToListAsync();
        return acceptedRequest;
    }

    public async Task<AcceptedRequest?> GetByIdWithServiceRequestAsync(Guid id)
    {
        return await dbContext.AcceptedRequests
            .Include(a => a.ServiceRequest)
                .ThenInclude(sr => sr.User)
            .FirstOrDefaultAsync(a => a.Id == id);
    }


    public async Task<AcceptedRequest?> GetByIdAsync(Guid id)
    {
        var acceptedRequest = await dbContext.AcceptedRequests
            .Include(c => c.Category)
            .Include(u => u.User)
            .Include(r => r.ServiceRequest)
            .FirstOrDefaultAsync(a => a.Id == id);

        return acceptedRequest;
    }

    public async Task<AcceptedRequest> UpdateAsync(AcceptedRequest acceptedRequest)
    {
        dbContext.AcceptedRequests.Update(acceptedRequest);
        await dbContext.SaveChangesAsync();
        return acceptedRequest;
    }
}
