using gizmogeo.Domain.Entities;

namespace gizmogeo.Domain.Interfaces;

public interface IAttachmentRepository
{
    Task<Attachment> CreateAsync(Attachment attachment);
    Task<IEnumerable<Attachment>> GetAllByServiceRequestIdAsync(Guid serviceRequestId);
    Task<Attachment?> GetByIdAsync(Guid id);
    Task DeleteAsync(Attachment attachment);
}
