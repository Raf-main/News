using System.ComponentModel.DataAnnotations;

namespace PRAS.Testovoe.Main.ViewModels.Request;

public class LoginRequest
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}