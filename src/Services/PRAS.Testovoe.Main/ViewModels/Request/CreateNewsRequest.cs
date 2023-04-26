using System.ComponentModel.DataAnnotations;
using PRAS.Testovoe.Main.Attributes;

namespace PRAS.Testovoe.Main.ViewModels.Request;

public class CreateNewsRequest
{
    [Required]
    [MinLength(10)]
    [MaxLength(40)]
    public string Title { get; set; } = null!;
    
    [Required]
    [MinLength(10)]
    [MaxLength(200)]
    public string Subtitle { get; set; } = null!;
    
    [Required]
    [MinLength(10)]
    [MaxLength(9999)]
    public string Text { get; set; } = null!;

    [Required]
    [AllowedExtensionsAttribute(".png")]
    public IFormFile Image { get; set; } = null!;
}