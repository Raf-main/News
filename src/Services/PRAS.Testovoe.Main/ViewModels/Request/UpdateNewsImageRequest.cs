using System.ComponentModel.DataAnnotations;
using PRAS.Testovoe.Main.Attributes;

namespace PRAS.Testovoe.Main.ViewModels.Request;

public class UpdateNewsImageRequest
{
    [Required]
    [AllowedExtensionsAttribute(".png")]
    public IFormFile Image { get; set; } = null!;
}