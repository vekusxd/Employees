using Employee.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employee.Api.Database.Configurations;

public class EmployeeFileConfiguration : IEntityTypeConfiguration<EmployeeFile>
{
    public void Configure(EntityTypeBuilder<EmployeeFile> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Path).HasMaxLength(256);
        builder.HasOne(e => e.Employee).WithMany(e => e.Files).HasForeignKey(e => e.EmployeeId);
        
        builder.Property(e => e.IsDeleted).HasDefaultValue(false);
        
        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}