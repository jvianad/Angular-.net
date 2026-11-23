using StaffHub.Domain.Common;

namespace StaffHub.Domain.Entities;

public class Employee : BaseEntity
{
    public string FullName {get;set;} = string.Empty;
    public string Email {get ;set;} = string.Empty;
    public decimal Salary { get; set;}
    public DateOnly HireDate  {get ;set;}
    public string? PhotoUrl  {get;set;}
    public Guid DepartmentId {get;set;}
    public Department? Department { get;set;}
}
