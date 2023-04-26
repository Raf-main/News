using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PRAS.Testovoe.Main.Models;

namespace PRAS.Testovoe.Main.Data.Contexts;

public class NewsDbContext : IdentityDbContext<User>
{
    public DbSet<News> NewsTable { get; set; } = null!;
    public DbSet<RefreshToken> RefreshTokenTable { get; set; } = null!;

    public NewsDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}