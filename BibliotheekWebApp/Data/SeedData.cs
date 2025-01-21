using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BibliotheekApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotheekApp.Data
{
    public static class SeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // Rollen seeden
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "User", NormalizedName = "USER" }
            );

            // Auteurs seeden
            modelBuilder.Entity<Auteur>().HasData(
                new Auteur { AuteurID = 1, Naam = "Auteur 1", GeboorteDatum = new DateTime(1975, 4, 10) },
                new Auteur { AuteurID = 2, Naam = "Auteur 2", GeboorteDatum = new DateTime(1980, 6, 5) },
                new Auteur { AuteurID = 3, Naam = "Auteur 3", GeboorteDatum = new DateTime(1990, 1, 1) },
                new Auteur { AuteurID = 4, Naam = "Auteur 4", GeboorteDatum = new DateTime(2000, 7, 15) }
            );

            // Boeken seeden
            modelBuilder.Entity<Boek>().HasData(
                new Boek { ISBN = "9781234567890", Titel = "Boek 1", Genre = "Fictie", PublicatieDatum = DateTime.Now, AuteurID = 1 },
                new Boek { ISBN = "9780987654321", Titel = "Boek 2", Genre = "Non-fictie", PublicatieDatum = DateTime.Now, AuteurID = 2 }
            );
        }

        public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            // Rollen toevoegen als ze niet bestaan
            string[] roles = { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Admin gebruiker toevoegen
            var adminEmail = "admin@example.com";
            if (userManager.Users.All(u => u.Email != adminEmail))
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    Voornaam = "Admin",
                    Achternaam = "Gebruiker",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Normale gebruiker toevoegen
            var userEmail = "user@example.com";
            if (userManager.Users.All(u => u.Email != userEmail))
            {
                var normalUser = new ApplicationUser
                {
                    UserName = userEmail,
                    Email = userEmail,
                    Voornaam = "Normaal",
                    Achternaam = "Gebruiker",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(normalUser, "User123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(normalUser, "User");
                }
            }
        }
    }
}
