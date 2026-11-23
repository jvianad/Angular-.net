using StaffHub.Domain.Entities;

namespace StaffHub.Application.Abstraction.Persistence;

public interface IDepartmentRepository : IRepository<Department>
{
    Task<bool> ExistsByNameAsync(string name, Guid? excludeId = null, CancellationToken cancellationToken = default);
    Task<bool> HasEmployeesAsync(Guid departmentId, CancellationToken cancellationToken = default);
}