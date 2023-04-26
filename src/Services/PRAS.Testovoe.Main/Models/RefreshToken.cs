using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Raf.Infrastructure.Shared.Repositories;

namespace PRAS.Testovoe.Main.Models;

public class RefreshToken : IHasKey<int>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Token { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public bool IsUsed { get; set; }
    public bool IsExpired => ExpirationTime > DateTime.UtcNow;
    public DateTime ExpirationTime { get; set; }

    [ForeignKey(nameof(UserId))]
    [InverseProperty(nameof(Models.User.RefreshTokens))]
    public virtual User User { get; set; } = null!;
}