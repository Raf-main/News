using Microsoft.Extensions.Options;
using Raf.Security.Shared.Jwt.Options;
using Raf.Utils.Shared.Time;

namespace PRAS.Testovoe.Main.Services.Auth;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtOptions _options;

    public RefreshTokenService(IDateTimeProvider dateTimeProvider, IOptions<JwtOptions> options)
    {
        _dateTimeProvider = dateTimeProvider;
        _options = options.Value;
    }

    public Models.RefreshToken GenerateRefreshToken(string userId, DateTime? expirationTime = null)
    {
        var expired = expirationTime ?? _dateTimeProvider.UtcNow().AddHours(_options.RefreshTokenExpirationTimeInHours);

        return new Models.RefreshToken
        {
            ExpirationTime = expired,
            Token = Guid.NewGuid().ToString(),
            UserId = userId
        };
    }
}