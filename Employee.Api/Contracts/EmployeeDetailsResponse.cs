namespace Employee.Api.Contracts;

public class EmployeeDetailsResponse
{
    public Guid Id { get; set;}
    public required string Username { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Patronymic { get; set; }
    public DateOnly Birthday { get; set; }
    public List<EmployeeFileResponse> Files { get; set; } = [];
}