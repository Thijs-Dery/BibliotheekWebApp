using Microsoft.EntityFrameworkCore;
using System;

namespace BibliotheekApp.Models
{
    public class BibliotheekContext : DbContext
    {
        public DbSet<Boek> Boeken { get; set; }
        public DbSet<Auteur> Auteurs { get; set; }
        public DbSet<Lid> Leden { get; set; }
        public DbSet<LidBoek> LidBoeken { get; set; }

        public BibliotheekContext(DbContextOptions<BibliotheekContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;Database=BibliotheekDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<Auteur>().HasData(
                new Auteur { AuteurID = 1, Naam = "Auteur 1", GeboorteDatum = new DateTime(1975, 4, 10) },
                new Auteur { AuteurID = 2, Naam = "Auteur 2", GeboorteDatum = new DateTime(1980, 6, 5) }
            );

            modelBuilder.Entity<Boek>().HasData(
                new Boek { ISBN = "9781402894626", Titel = "Frieda Kroket", Genre = "Koken", PublicatieDatum = new DateTime(2020, 1, 1), AuteurID = 1 },
                new Boek { ISBN = "9783161484100", Titel = "Koken met Henk", Genre = "Koken", PublicatieDatum = new DateTime(2021, 2, 2), AuteurID = 2 }
            );

            modelBuilder.Entity<Lid>().HasData(
                new Lid { LidID = 1, Naam = "Freddy", GeboorteDatum = new DateTime(1990, 5, 1) },
                new Lid { LidID = 2, Naam = "Jochim", GeboorteDatum = new DateTime(1985, 3, 15) }
            );

            modelBuilder.Entity<LidBoek>().HasData(
                new LidBoek { LidBoekID = 1, LidID = 1, ISBN = "9781402894626", UitleenDatum = new DateTime(2023, 1, 15), InleverDatum = new DateTime(2023, 2, 15) }
            );
        }
    }
}

