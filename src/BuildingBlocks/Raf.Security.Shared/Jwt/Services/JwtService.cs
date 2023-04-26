using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Raf.Security.Shared.Jwt.Options;
using Raf.Utils.Shared.Time;

namespace Raf.Security.Shared.Jwt.Services;

public class JwtService : IJwtService
{
    private readonly JwtOptions _jwtOptions;
    private readonly IDateTimeProvider _dateTimeProvider;

    public JwtService(IOptions<JwtOptions> jwtOptions, IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtOptions = jwtOptions.Value;
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims, DateTime? expirationTime = null)
    {
        var secretKey = _jwtOptions.GetSymmetricSecurityKey();
        var securityAlgorithm = _jwtOptions.Algorithm ?? SecurityAlgorithms.HmacSha256;
        var signinCredentials = new SigningCredentials(secretKey, securityAlgorithm);
        var expires = expirationTime ?? _dateTimeProvider.UtcNow().AddMinutes(_jwtOptions.TokenExpirationTimeInMinutes);

        var tokenOptions = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            expires,
            signingCredentials: signinCredentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return token;
    }
}