namespace PRAS.Testovoe.Main.Services.Auth;

public interface IRefreshTokenService
{
    Models.RefreshToken GenerateRefreshToken(string userId, DateTime? expirationTime = null);
}