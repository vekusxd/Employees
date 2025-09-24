using Microsoft.AspNetCore.Identity;

namespace Employee.Api.Models;

public class ApplicationUser : IdentityUser<Guid>
{
    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
}