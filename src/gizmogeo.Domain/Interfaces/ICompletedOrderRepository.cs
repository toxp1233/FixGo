using gizmogeo.Domain.Entities;

namespace gizmogeo.Domain.Interfaces;

public interface ICompletedOrderRepository
{
    Task<IEnumerable<CompletedOrder>> GetAllAsync();
    Task<CompletedOrder?> GetByIdAsync(Guid id);
    Task<IEnumerable<CompletedOrder?>> GetByAdmin(Guid adminId);
    Task<CompletedOrder> CreateAsync(CompletedOrder record);
    Task<CompletedOrder> UpdateAsync(CompletedOrder record);
    IQueryable<CompletedOrder> GetQueryable();
}
