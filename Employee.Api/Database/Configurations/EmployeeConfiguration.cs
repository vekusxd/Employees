using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employee.Api.Database.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Models.Employee>
{
    public void Configure(EntityTypeBuilder<Models.Employee> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Username).HasMaxLength(50);
        builder.Property(e => e.FirstName).HasMaxLength(50);
        builder.Property(e => e.LastName).HasMaxLength(50);
        builder.Property(e => e.Patronymic).HasMaxLength(50);
        builder.HasMany(e => e.Files).WithOne(e => e.Employee).HasForeignKey(e => e.EmployeeId);
        
        builder.Property(e => e.IsDeleted).HasDefaultValue(false);
        
        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}