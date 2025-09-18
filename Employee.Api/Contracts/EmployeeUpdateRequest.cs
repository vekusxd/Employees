namespace Employee.Api.Contracts;

public record EmployeeUpdateRequest(
    string? Username,
    string? FirstName,
    string? LastName,
    string? Patronymic,
    DateOnly? Birthday
);