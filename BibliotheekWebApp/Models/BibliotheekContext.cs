using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace BibliotheekApp.Models
{
    public class BibliotheekContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Boek> Boeken { get; set; }
        public DbSet<Auteur> Auteurs { get; set; }
        public DbSet<Lid> Leden { get; set; }
        public DbSet<LidBoek> LidBoeken { get; set; }

        public BibliotheekContext(DbContextOptions<BibliotheekContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relaties configureren
            modelBuilder.Entity<LidBoek>()
                .HasKey(lb => lb.LidBoekID);

            modelBuilder.Entity<LidBoek>()
                .HasOne(lb => lb.Lid)
                .WithMany(l => l.GeleendeBoeken)
                .HasForeignKey(lb => lb.LidID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LidBoek>()
                .HasOne(lb => lb.Boek)
                .WithMany(b => b.LidBoeken)
                .HasForeignKey(lb => lb.ISBN)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Boek>()
                .HasOne(b => b.Auteur)
                .WithMany(a => a.Boeken)
                .HasForeignKey(b => b.AuteurID)
                .OnDelete(DeleteBehavior.Cascade);

            // Rollen seeden
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" }
            );

            // Gebruikers seeden
            var hasher = new PasswordHasher<ApplicationUser>();
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "1",
                    UserName = "admin@example.com",
                    NormalizedUserName = "ADMIN@EXAMPLE.COM",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin123!"),
                    Voornaam = "Admin",
                    Achternaam = "User"
                },
                new ApplicationUser
                {
                    Id = "2",
                    UserName = "user@example.com",
                    NormalizedUserName = "USER@EXAMPLE.COM",
                    Email = "user@example.com",
                    NormalizedEmail = "USER@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "User123!"),
                    Voornaam = "Regular",
                    Achternaam = "User"
                }
            );

            // Gebruikersrollen koppelen
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = "1", RoleId = "1" }, // Admin -> Admin Role
                new IdentityUserRole<string> { UserId = "2", RoleId = "2" }  // User -> User Role
            );

            // Tabellen seeden
            modelBuilder.Entity<Auteur>().HasData(
                new Auteur { AuteurID = 1, Naam = "Auteur 1", GeboorteDatum = new DateTime(1975, 4, 10) },
                new Auteur { AuteurID = 2, Naam = "Auteur 2", GeboorteDatum = new DateTime(1980, 6, 5) }
            );

            modelBuilder.Entity<Boek>().HasData(
                new Boek { ISBN = "9781402894626", Titel = "Frieda Kroket", Genre = "Koken", PublicatieDatum = DateTime.Now, AuteurID = 1 },
                new Boek { ISBN = "9783161484100", Titel = "Koken met Henk", Genre = "Koken", PublicatieDatum = DateTime.Now, AuteurID = 2 }
            );
        }
    }
}
