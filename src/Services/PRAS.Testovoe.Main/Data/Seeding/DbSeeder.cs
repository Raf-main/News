using Microsoft.AspNetCore.Identity;
using PRAS.Testovoe.Main.Models;

namespace PRAS.Testovoe.Main.Data.Seeding;

public static class DataSeeder
{
    public static async Task SeedData(this IApplicationBuilder app)
    {
        using(var scope = app.ApplicationServices.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetService<UserManager<User>>()!;
            var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>()!;

            foreach(var role in GetRoles())
            {
                var exists = await roleManager.RoleExistsAsync(role.Name!);

                if (!exists)
                {
                    await roleManager.CreateAsync(role);
                }
            }

            foreach(var user in GetUsers())
            {
                var foundUser = await userManager.FindByEmailAsync(user.Email!);
                
                if (foundUser == null)
                {
                await userManager.CreateAsync(user);
                }
            }

            foreach(var userRole in GetUserRoles())
            {
                var user = await userManager.FindByIdAsync(userRole.UserId);
                var role = await roleManager.FindByIdAsync(userRole.RoleId);
                
                if(!await userManager.IsInRoleAsync(user!, role!.Name!))
                {
                    await userManager.AddToRoleAsync(user!, role.Name!);
                }
            }
        }
    }

    public static IEnumerable<User> GetUsers()
    {
        var passwordHasher = new PasswordHasher<User>();

        yield return new User
        {
            Id = "beb20912-705f-4788-83cb-b5498b1d1bd5",
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@gmail.com",
            NormalizedEmail = "ADMIN@GMAIL.COM",
            PasswordHash = passwordHasher.HashPassword(null, "admin")
        };
    }

    public static IEnumerable<IdentityRole> GetRoles()
    {
        yield return new IdentityRole
        {
            Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", 
            Name = "Admin", 
            NormalizedName = "ADMIN" 
        };
    }

    public static IEnumerable<IdentityUserRole<string>> GetUserRoles()
    {
        yield return new IdentityUserRole<string>
        {
            RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210", 
            UserId = "beb20912-705f-4788-83cb-b5498b1d1bd5"    
        };
    }
}