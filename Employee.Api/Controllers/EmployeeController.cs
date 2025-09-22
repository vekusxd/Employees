using Employee.Api.Contracts;
using Employee.Api.Database;
using Employee.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee.Api.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeeController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly IWebHostEnvironment _env;

    public EmployeeController(AppDbContext dbContext, IWebHostEnvironment env)
    {
        _dbContext = dbContext;
        _env = env;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEmployee()
    {
        var employees = await _dbContext.Employees
            .AsNoTracking()
            .Select(e => new EmployeeResponse
            {
                Id = e.Id,
                Birthday = e.Birthday,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Username = e.Username,
                Patronymic = e.Patronymic
            }).ToListAsync();
        return Ok(employees);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetEmployeeById(Guid id)
    {
        var employee = await _dbContext.Employees
            .Select(e => new EmployeeDetailsResponse
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Patronymic = e.Patronymic,
                Birthday = e.Birthday,
                Username = e.Username,
                Files = e.Files.Select(ef => new EmployeeFileResponse
                {
                    Id = ef.Id,
                    Path = ef.Path,
                    Created = ef.Created,
                }).ToList()
            })
            .FirstOrDefaultAsync(e => e.Id == id);

        if (employee is null)
            return NotFound();

        return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] EmployeeRequest request)
    {
        var employee = new Models.Employee
        {
            Username = request.Username,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Patronymic = request.Patronymic,
            Birthday = request.Birthday,
        };

        _dbContext.Employees.Add(employee);
        await _dbContext.SaveChangesAsync();

        return Ok(employee);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] EmployeeUpdateRequest request)
    {
        var employee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);

        if (employee is null)
            return NotFound();

        employee.Username = request?.Username ?? employee.Username;
        employee.FirstName = request?.FirstName ?? employee.FirstName;
        employee.LastName = request?.LastName ?? employee.LastName;
        employee.Patronymic = request?.Patronymic ?? employee.Patronymic;
        employee.Birthday = request?.Birthday ?? employee.Birthday;

        await _dbContext.SaveChangesAsync();

        return Ok(employee);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEmployee(Guid id)
    {
        var employee = await _dbContext.Employees.Include(e => e.Files).FirstOrDefaultAsync(e => e.Id == id);

        if (employee is null)
            return NotFound();

        employee.IsDeleted = true;

        foreach (var file in employee.Files)
            file.IsDeleted = true;

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost("{id:guid}/link-file")]
    public async Task<IActionResult> LinkFile(Guid id, [FromForm] IFormFile file)
    {
        var employee = await _dbContext.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

        if (employee is null)
            return NotFound();

        var webRootPath = _env.WebRootPath;
        var randomFileName = Guid.NewGuid().ToString("N");

        var extension = Path.GetExtension(file.FileName);

        var serverPath = $"uploads/{randomFileName}{extension}";

        var path = Path.Combine(webRootPath, serverPath);
        await using var stream = System.IO.File.Create(path);
        await file.CopyToAsync(stream);

        var employeeFile = new EmployeeFile
        {
            Created = DateTime.UtcNow,
            EmployeeId = employee.Id,
            Path = serverPath,
        };

        _dbContext.EmployeeFiles.Add(employeeFile);
        await _dbContext.SaveChangesAsync();

        return Ok(employeeFile);
    }

    [HttpGet("files/{fileId:guid}")]
    public async Task<IActionResult> GetEmployeeFile(Guid fileId, CancellationToken cancellationToken)
    {
        var file = _dbContext.EmployeeFiles.AsNoTracking().FirstOrDefault(e => e.Id == fileId);

        if (file is null)
            return NotFound();

        var filePath = Path.Combine(_env.WebRootPath, file.Path);
        
        var contentType = MimeMapping.MimeUtility.GetMimeMapping(file.Path);
        var fileContent = await System.IO.File.ReadAllBytesAsync(filePath, cancellationToken);
       return File(fileContent, contentType);
    }

    [HttpDelete("files/{fileId:guid}")]
    public async Task<IActionResult> DeleteFile(Guid fileId, CancellationToken cancellationToken)
    {
        var file = _dbContext.EmployeeFiles.FirstOrDefault(e => e.Id == fileId);
        
        if (file is null)
            return NotFound();

        file.IsDeleted = true;
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return NoContent();
    }

    [HttpGet("files/{fileId:guid}/preview")]
    public async Task<IActionResult> PreviewEmployeeFile(Guid fileId, CancellationToken cancellationToken)
    {
        var file = _dbContext.EmployeeFiles.AsNoTracking().FirstOrDefault(e => e.Id == fileId);

        if (file is null)
            return NotFound();

        var filePath = Path.Combine(_env.WebRootPath, file.Path);
        
        if (!System.IO.File.Exists(filePath))
            return NotFound();

        var contentType = MimeMapping.MimeUtility.GetMimeMapping(file.Path);
        var fileContent = await System.IO.File.ReadAllBytesAsync(filePath, cancellationToken);
        
        return File(fileContent, contentType, fileDownloadName: null);
    }
}