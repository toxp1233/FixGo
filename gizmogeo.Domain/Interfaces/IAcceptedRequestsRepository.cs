using gizmogeo.Domain.Entities;

namespace gizmogeo.Domain.Interfaces;

public interface IAcceptedRequestsRepository
{
    Task<IEnumerable<AcceptedRequest>> GetAllAsync();
    Task<AcceptedRequest> CreateAsync(AcceptedRequest acceptedRequest);
    Task<AcceptedRequest> UpdateAsync(AcceptedRequest acceptedRequest);
    Task DeleteAsync(AcceptedRequest acceptedRequest);
    Task<AcceptedRequest?> GetByIdAsync(Guid id);
    Task<IEnumerable<AcceptedRequest>> GetByUserIdAsync(Guid adminId);
    Task<AcceptedRequest?> GetByIdWithServiceRequestAsync(Guid id);
}
