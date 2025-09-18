namespace Employee.Api.Contracts;

public record EmployeeRequest(
    string Username,
    string FirstName,
    string LastName,
    string Patronymic,
    DateOnly Birthday
    );