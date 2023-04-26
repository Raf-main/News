using Microsoft.AspNetCore.Http;

namespace Raf.Utils.Shared.Cookie;

public interface ICookieService
{
    void SetResponseCookie(string key, string value, DateTime expirationDate, bool httpOnly = false,
        SameSiteMode sameSite = SameSiteMode.None);

    bool TryGetRequestCookie(string key, out string? value);
}