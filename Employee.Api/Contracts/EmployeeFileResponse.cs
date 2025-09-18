namespace Employee.Api.Contracts;

public class EmployeeFileResponse
{
    public Guid Id { get; set; }
    public required string Path { get; set; }
    public DateTime Created { get; set; }
}