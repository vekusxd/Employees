namespace Employee.Api.Models;

public class EmployeeFile
{
    public Guid Id { get; set; }
    public string Path { get; set; }
    public DateTime Created { get; set; }
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public bool IsDeleted { get; set; }
}