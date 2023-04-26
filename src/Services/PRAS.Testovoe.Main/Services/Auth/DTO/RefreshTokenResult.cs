using PRAS.Testovoe.Main.ViewModels.Response;

namespace PRAS.Testovoe.Main.Services.Auth.DTO;
public class RefreshTokenResult
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public DateTime RefreshTokenExpirationTime { get; set; }
    public UserResponse User { get; set; } = null!;
}