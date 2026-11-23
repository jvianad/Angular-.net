using StaffHub.Domain.Common;

namespace StaffHub.Domain.Entities;

public class Employee : BaseEntity
{
    public string FullName {get;set;}
    public string Email {get ;set;}
    public decimal Salary { get; set;}
    public DateOnly HireDate  {get ;set;}
    public string? PhotoUrl  {get;set;}
    public Guid DepartmentId {get;set;}
    public Department? Department { get;set;}
}
