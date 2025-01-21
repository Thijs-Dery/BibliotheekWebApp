using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BibliotheekApp.Models;
using System;

namespace BibliotheekApp.Data
{
    public static class SeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // Rollen seeden
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" }
            );

            // Auteurs seeden
            modelBuilder.Entity<Auteur>().HasData(
                new Auteur { AuteurID = 1, Naam = "Auteur 1", GeboorteDatum = new DateTime(1975, 4, 10) },
                new Auteur { AuteurID = 2, Naam = "Auteur 2", GeboorteDatum = new DateTime(1980, 6, 5) }
            );

            // Boeken seeden (Gebruik een vaste PublicatieDatum)
            var fixedDate = new DateTime(2023, 1, 1); // Vaste datum voor consistentie
            modelBuilder.Entity<Boek>().HasData(
                new Boek { ISBN = "9781402894626", Titel = "Frieda Kroket", Genre = "Koken", PublicatieDatum = fixedDate, AuteurID = 1 },
                new Boek { ISBN = "9783161484100", Titel = "Koken met Henk", Genre = "Koken", PublicatieDatum = fixedDate, AuteurID = 2 }
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
            if (!userManager.Users.Any(u => u.UserName == "admin@example.com"))
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "admin@example.com",
                    Email = "admin@example.com",
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
            if (!userManager.Users.Any(u => u.UserName == "user@example.com"))
            {
                var normalUser = new ApplicationUser
                {
                    UserName = "user@example.com",
                    Email = "user@example.com",
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
