using Employee.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employee.Api.Database.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(t => t.Id);
        
        builder.Property(t => t.Token).HasMaxLength(128);
        
        builder
            .HasOne(t => t.User)
            .WithMany(t => t.RefreshTokens)
            .HasForeignKey(t => t.UserId);
    }
}