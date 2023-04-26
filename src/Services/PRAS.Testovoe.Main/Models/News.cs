using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Raf.Infrastructure.Shared.Repositories;

namespace PRAS.Testovoe.Main.Models;

public class News : IHasKey<int>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Subtitle { get; set; } = null!;
    public string Text { get; set; } = null!;
    public string ImageName { get; set; } = null!;
    public DateTime CreatedOn { get; set; }
}