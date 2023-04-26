using System.ComponentModel.DataAnnotations;

namespace PRAS.Testovoe.Main.ViewModels.Request;

public class RegistrationRequest
{
    [Required]
    [MinLength(4)]
    [MaxLength(20)]
    public string UserName { get; set; } = null!;

    [Required] 
    [EmailAddress] 
    public string Email { get; set; } = null!;

    [Required]
    [MinLength(4)]
    [MaxLength(20)]
    public string Password { get; set; } = null!;
}