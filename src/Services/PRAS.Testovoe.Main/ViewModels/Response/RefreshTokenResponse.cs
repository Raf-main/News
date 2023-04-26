namespace PRAS.Testovoe.Main.ViewModels.Response;

public class RefreshTokenResponse
{
    public string AccessToken { get; set; } = null!;
    public UserResponse User { get; set; } = null!;
}