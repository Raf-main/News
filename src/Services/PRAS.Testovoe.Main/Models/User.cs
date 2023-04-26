using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRAS.Testovoe.Main.Models;

public class User : IdentityUser
{
    [InverseProperty(nameof(RefreshToken.User))]
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>();
}