using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Raf.Security.Shared.Jwt.Options;

public class JwtOptions
{
    public string? Algorithm { get; set; }
    public string SecretKey { get; set; } = null!;
    public bool ValidateIssuer { get; set; }
    public bool ValidateAudience { get; set; }
    public int TokenExpirationTimeInMinutes { get; set; }
    public int RefreshTokenExpirationTimeInHours { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
    }
}