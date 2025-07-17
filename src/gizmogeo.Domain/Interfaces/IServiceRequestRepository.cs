using gizmogeo.Domain.Entities;
namespace gizmogeo.Domain.Interfaces;

public interface IServiceRequestRepository
{
    Task<IEnumerable<ServiceRequest>> GetAllAsync();
    Task<ServiceRequest> CreateAsync(ServiceRequest request);
    Task<ServiceRequest> UpdateAsync(ServiceRequest request);
    Task DeleteAsync(ServiceRequest request);
    Task<ServiceRequest?> GetByIdAsync(Guid id);
    Task<IEnumerable<ServiceRequest>> GetByUserId(Guid userId);
    IQueryable<ServiceRequest> GetQueryable();
}
