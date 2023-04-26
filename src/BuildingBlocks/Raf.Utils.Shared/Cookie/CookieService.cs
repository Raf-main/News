using Microsoft.AspNetCore.Http;

namespace Raf.Utils.Shared.Cookie;

public class CookieService : ICookieService
{
    private readonly HttpContext _httpContext;

    public CookieService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContext = httpContextAccessor.HttpContext;
    }

    public void SetResponseCookie(string key, string value, DateTime expirationDate, bool httpOnly = false,
        SameSiteMode sameSite = SameSiteMode.None)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = httpOnly,
            SameSite = sameSite,
            Expires = expirationDate
        };

        _httpContext.Response.Cookies.Append(key, value, cookieOptions);
    }

    public bool TryGetRequestCookie(string key, out string? value)
    {
        return _httpContext.Request.Cookies.TryGetValue(key, out value);
    }
}