using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using BibliotheekApp.Models;

namespace BibliotheekApp.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            string[] roles = { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var adminUser = new ApplicationUser
            {
                UserName = "admin@example.com",
                Email = "admin@example.com",
                Voornaam = "Admin",
                Achternaam = "Gebruiker",
                EmailConfirmed = true
            };

            if (userManager.Users.All(u => u.UserName != adminUser.UserName))
            {
                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            var normalUser = new ApplicationUser
            {
                UserName = "user@example.com",
                Email = "user@example.com",
                Voornaam = "Normaal",
                Achternaam = "Gebruiker",
                EmailConfirmed = true
            };

            if (userManager.Users.All(u => u.UserName != normalUser.UserName))
            {
                var result = await userManager.CreateAsync(normalUser, "User123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(normalUser, "User");
                }
            }
        }
    }
}
