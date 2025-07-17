namespace gizmogeo.Domain.Interfaces;

public interface IUserRepository
{
    Task<User> CreateAsync(User user);
    Task<User> UpdateAsync(User user);
    Task DeleteAsync(User user);
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByNameAsync(string name);
    Task<User?> GetByPhoneNumberAsync(string phoneNumber);
    Task<User?> GetByPendingPhoneNumberAsync(string phoneNumber);
    IQueryable<User> GetQueryable();
}
