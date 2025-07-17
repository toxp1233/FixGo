using gizmogeo.Domain.Entities;
using gizmogeo.Domain.Interfaces;
using gizmogeo.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace gizmogeo.Infrastructure.Repositories;

public class ServiceRequestRepository(FixGoDbContext dbContext) : IServiceRequestRepository
{
    public async Task<ServiceRequest> CreateAsync(ServiceRequest request)
    {
        await dbContext.AddAsync(request);
        await dbContext.SaveChangesAsync();
        return request;
    }

    public async Task DeleteAsync(ServiceRequest request)
    {
        dbContext.ServiceRequests.Remove(request);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<ServiceRequest>> GetAllAsync()
    {
        var serviceRequests = await dbContext.ServiceRequests
            .Include(u => u.User)
            .Include(c => c.Category)
            .Include(a => a.AcceptedRequest)
            .ToListAsync();
        return serviceRequests;
    }

    public async Task<ServiceRequest?> GetByIdAsync(Guid id)
    {
        var serviceRequest = await dbContext.ServiceRequests
            .Include(u => u.User)
            .Include(c => c.Category)
            .Include(a => a.AcceptedRequest)
            .FirstOrDefaultAsync(x => x.Id == id);
        return serviceRequest;
    }

    public async Task<IEnumerable<ServiceRequest>> GetByUserId(Guid userId)
    {
        var serviceRequests = await dbContext.ServiceRequests
            .Where(u => u.UserId == userId)
            .Include(u => u.User)
            .Include(c => c.Category)
            .Include(a => a.AcceptedRequest)
            .ToListAsync();
        return serviceRequests;
    }

    public IQueryable<ServiceRequest> GetQueryable()
    {
        return dbContext.ServiceRequests.AsQueryable();
    }

    public async Task<ServiceRequest> UpdateAsync(ServiceRequest request)
    {
        dbContext.ServiceRequests.Update(request);
        await dbContext.SaveChangesAsync();
        return request;
    }


}
