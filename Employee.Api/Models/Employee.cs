namespace Employee.Api.Models;

public class Employee
{
    public Guid Id { get; set; }
    public required string Username { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Patronymic { get; set; }
    public DateOnly Birthday { get; set; }
    public ICollection<EmployeeFile> Files { get; set; } = [];
    public bool IsDeleted { get; set; }
}