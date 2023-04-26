namespace PRAS.Testovoe.Main.Services.Auth.DTO;

public class LoginResult
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public DateTime RefreshTokenExpirationTime { get; set; }
}