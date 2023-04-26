using System.Security.Claims;

namespace Raf.Security.Shared.Jwt.Services;

public interface IJwtService
{
    string GenerateAccessToken(IEnumerable<Claim> claims, DateTime? expirationTime = null);
}