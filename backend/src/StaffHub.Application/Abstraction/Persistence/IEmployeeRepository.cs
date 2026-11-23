using StaffHub.Domain.Entities;

namespace StaffHub.Application.Abstraction.Persistence;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<(List<Employee> Items, int TotalCount)> GetPagedAsync(
        string? search,
        Guid? departmentId,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);

    Task<Employee?> GetByIdWithDepartmentAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByEmailAsync(string email, Guid? excludeId = null, CancellationToken cancellationToken = default);
}