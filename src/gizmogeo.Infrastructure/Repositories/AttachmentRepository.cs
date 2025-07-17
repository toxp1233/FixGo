using gizmogeo.Domain.Entities;
using gizmogeo.Domain.Interfaces;
using gizmogeo.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace gizmogeo.Infrastructure.Repositories;

public class AttachmentRepository(FixGoDbContext dbContext) : IAttachmentRepository
{
    public async Task<Attachment> CreateAsync(Attachment attachment)
    {
        await dbContext.Attachments.AddAsync(attachment);
        await dbContext.SaveChangesAsync();
        return attachment;
    }

    public async Task<IEnumerable<Attachment>> GetAllByServiceRequestIdAsync(Guid serviceRequestId)
    {
        return await dbContext.Attachments
            .Where(a => a.ServiceRequestId == serviceRequestId)
            .Include(r => r.ServiceRequest)
            .ToListAsync();
    }

    public async Task<Attachment?> GetByIdAsync(Guid id)
    {
        return await dbContext.Attachments
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task DeleteAsync(Attachment attachment)
    {
        dbContext.Attachments.Remove(attachment);
        await dbContext.SaveChangesAsync();
    }
}
