using StaffHub.Domain.Common;

namespace StaffHub.Application.Abstraction.Persistence;

public interface IRepository<T> where T : BaseEntity
{
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);
    void Update(T entity);
    void Remove(T entity);
}