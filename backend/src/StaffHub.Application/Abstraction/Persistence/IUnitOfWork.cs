using StaffHub.Domain.Common;

namespace StaffHub.Application.Abstraction.Persistence;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}