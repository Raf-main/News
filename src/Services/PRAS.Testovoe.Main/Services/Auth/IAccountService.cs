using PRAS.Testovoe.Main.Services.Auth.DTO;
using PRAS.Testovoe.Main.ViewModels.Request;

namespace PRAS.Testovoe.Main.Services.Auth;

public interface IAccountService
{
    Task<LoginResult> LoginAsync(LoginRequest loginRequest);
    Task RegisterAsync(RegistrationRequest registrationRequest);
    Task<RefreshTokenResult> RefreshTokenAsync(RefreshTokenRequest refreshRequest);
    Task<IEnumerable<string>> GetRolesAsync(string email);
    Task<bool> EmailExists(string email);
}