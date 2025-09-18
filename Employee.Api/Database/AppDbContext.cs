using System.Reflection;
using Employee.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee.Api.Database;

public class AppDbContext : DbContext
{
    public DbSet<Models.Employee> Employees { get; set; }
    public DbSet<EmployeeFile> EmployeeFiles { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}