using Microsoft.AspNetCore.Mvc;
using PRAS.Testovoe.Main.Services.Auth;
using PRAS.Testovoe.Main.ViewModels.Request;
using PRAS.Testovoe.Main.ViewModels.Response;
using Raf.Utils.Shared.Cookie;

namespace PRAS.Testovoe.Main.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AccountController : ControllerBase
{
    private const string RefreshTokenCookieKey = "RefreshToken";
    private readonly IAccountService _accountService;
    private readonly ICookieService _cookieService;

    public AccountController(IAccountService accountService,
        ICookieService cookieService)
    {
        _accountService = accountService;
        _cookieService = cookieService;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.Root.Errors.Select(e => e.ErrorMessage));
        }

        var loginResult = await _accountService.LoginAsync(loginRequest);

        _cookieService.SetResponseCookie(RefreshTokenCookieKey, loginResult.RefreshToken,
            loginResult.RefreshTokenExpirationTime, true, SameSiteMode.Strict);

        var user = new UserResponse
        {
            Roles = await _accountService.GetRolesAsync(loginRequest.Email),
        };

        var loginResponse = new LoginResponse
        {
            AccessToken = loginResult.AccessToken,
            User = user
        };

        return Ok(loginResponse);
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegistrationRequest registrationRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.Root.Errors.Select(e => e.ErrorMessage));
        }

        await _accountService.RegisterAsync(registrationRequest);

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> RefreshAccessToken()
    {
        if(!_cookieService.TryGetRequestCookie(RefreshTokenCookieKey, out var refreshToken))
        {
            return Forbid();
        }
        
        if (string.IsNullOrEmpty(refreshToken))
        {
            return Forbid();
        }

        var refreshTokenRequest = new RefreshTokenRequest
        {
            RefreshToken = refreshToken
        };

        var refreshTokenResult = await _accountService.RefreshTokenAsync(refreshTokenRequest);

        _cookieService.SetResponseCookie(RefreshTokenCookieKey, refreshTokenResult.RefreshToken,
            refreshTokenResult.RefreshTokenExpirationTime,
            true, SameSiteMode.Strict);

        var refreshTokenResponse = new RefreshTokenResponse
        {
            AccessToken = refreshTokenResult.AccessToken,
            User = refreshTokenResult.User
        };

        return Ok(refreshTokenResponse);
    }

    [HttpGet]
    public async Task<IActionResult> EmailExists(string email)
    {
        if(string.IsNullOrEmpty(email))
        {
            return BadRequest();
        }

        var result = await _accountService.EmailExists(email);

        return Ok(result);
    }
}