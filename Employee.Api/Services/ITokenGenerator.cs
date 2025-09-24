using Employee.Api.Models;

namespace Employee.Api.Services;

public interface ITokenGenerator
{
    public string GenerateAccessToken(ApplicationUser user);
    public string GenerateRefreshToken();
}