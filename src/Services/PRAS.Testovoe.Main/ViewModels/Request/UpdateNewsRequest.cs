using System.ComponentModel.DataAnnotations;

namespace PRAS.Testovoe.Main.ViewModels.Request;

public class UpdateNewsRequest
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
}