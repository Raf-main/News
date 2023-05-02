using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PRAS.Testovoe.Main.Exceptions;
using PRAS.Testovoe.Main.Models;
using PRAS.Testovoe.Main.Services.Auth.DTO;
using PRAS.Testovoe.Main.ViewModels.Request;
using PRAS.Testovoe.Main.Data.Repositories;
using Raf.Security.Shared.Jwt.Services;
using PRAS.Testovoe.Main.Services.Auth.Specifications;
using PRAS.Testovoe.Main.ViewModels.Response;

namespace PRAS.Testovoe.Main.Services.Auth;

public class AccountService : IAccountService
{
    private readonly IJwtService _jwtService;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public AccountService(IUnitOfWork unitOfWork,
        IJwtService jwtService,
        IRefreshTokenService refreshTokenService,
        UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
        _refreshTokenService = refreshTokenService;
        _userManager = userManager;
    }

    public async Task<LoginResult> LoginAsync(LoginRequest loginRequest)
    {
        var user = await _userManager.FindByEmailAsync(loginRequest.Email);

        if (user == null)
        {
            throw new NotFoundException($"User with email {loginRequest.Email} was not found");
        }

        var authClaims = await GetClaimsAsync(user);

        var accessToken = _jwtService.GenerateAccessToken(authClaims);
        var refreshToken = _refreshTokenService.GenerateRefreshToken(user.Id.ToString());

        await _unitOfWork.RefreshTokens.AddAsync(refreshToken);
        await _unitOfWork.SaveChangesAsync();

        return new LoginResult
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
            RefreshTokenExpirationTime = refreshToken.ExpirationTime
        };
    }

    public async Task RegisterAsync(RegistrationRequest registrationRequest)
    {
        var user = new User
        {
            UserName = registrationRequest.UserName,
            Email = registrationRequest.Email
        };

        var result = await _userManager.CreateAsync(user, registrationRequest.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);

            throw new ValidationException(
                $"Something went wrong while creating user with email {registrationRequest.Email}");
        }
    }

    public async Task<RefreshTokenResult> RefreshTokenAsync(RefreshTokenRequest refreshRequest)
    {
        var tokens = await _unitOfWork.RefreshTokens.GetAsync(new ByTokenSpecification(refreshRequest.RefreshToken), true);
        var refreshToken = tokens.FirstOrDefault();

        if (refreshToken == null || refreshToken.IsUsed)
        {
            throw new NotFoundException($"Refresh token was not found");
        }

        if (refreshToken.IsExpired)
        {
            throw new SecurityTokenExpiredException("Refresh token was expired");
        }

        var user = await _userManager.FindByIdAsync(refreshToken.UserId);

        if (user == null)
        {
            throw new NotFoundException($"User was not found");
        }

        var authClaims = await GetClaimsAsync(user);
        var newAccessToken = _jwtService.GenerateAccessToken(authClaims);
        var newRefreshToken = _refreshTokenService.GenerateRefreshToken(user.Id);

        refreshToken.IsUsed = true;
        await _unitOfWork.RefreshTokens.UpdateAsync(refreshToken);
        await _unitOfWork.RefreshTokens.AddAsync(newRefreshToken);
        await _unitOfWork.SaveChangesAsync();

        return new RefreshTokenResult()
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken.Token,
            RefreshTokenExpirationTime = newRefreshToken.ExpirationTime,
            User = new UserResponse { Roles = await GetRolesAsync(user.Email) }
        };
    }

    public async Task<IEnumerable<string>> GetRolesAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

        return await _userManager.GetRolesAsync(user);
    }

    public async Task<bool> EmailExists(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        return user != null;
    }

    private async Task<IEnumerable<Claim>> GetClaimsAsync(User user)
    {
        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var userRoles = await _userManager.GetRolesAsync(user);

        if (userRoles is { Count: > 0 })
        {
            authClaims.AddRange(userRoles.Where(role => !string.IsNullOrEmpty(role))
                .Select(role => new Claim(ClaimTypes.Role, role)));
        }

        return authClaims;
    }
}