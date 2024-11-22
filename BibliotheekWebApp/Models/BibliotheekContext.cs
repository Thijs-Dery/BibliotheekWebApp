using Microsoft.EntityFrameworkCore;
using BibliotheekApp.Models;
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
                new Auteur { AuteurID = 2, Naam = "Auteur 2", GeboorteDatum = new DateTime(1980, 6, 5) },
                new Auteur { AuteurID = 3, Naam = "Auteur 3", GeboorteDatum = new DateTime(1995, 9, 18) },
                new Auteur { AuteurID = 4, Naam = "Auteur 4", GeboorteDatum = new DateTime(1978, 11, 23) }
            );

            modelBuilder.Entity<Boek>().HasData(
                new Boek { ISBN = "9781402894626", Titel = "Frieda Kroket", Genre = "Koken", PublicatieDatum = new DateTime(2020, 1, 1), AuteurID = 1 },
                new Boek { ISBN = "9783161484100", Titel = "Koken met Henk", Genre = "Koken", PublicatieDatum = new DateTime(2021, 2, 2), AuteurID = 2 },
                new Boek { ISBN = "TEST-010e1999", Titel = "Wdsawd", Genre = "Fictie", PublicatieDatum = new DateTime(2022, 3, 3), AuteurID = 3 },
                new Boek { ISBN = "TEST-0001", Titel = "De Avonturen van Bob", Genre = "Avontuur", PublicatieDatum = new DateTime(2019, 5, 6), AuteurID = 4 }
            );

            modelBuilder.Entity<Lid>().HasData(
                new Lid { LidID = 1, Naam = "Freddy", GeboorteDatum = new DateTime(1990, 5, 1) },
                new Lid { LidID = 2, Naam = "Jochim", GeboorteDatum = new DateTime(1985, 3, 15) },
                new Lid { LidID = 3, Naam = "Jos", GeboorteDatum = new DateTime(2000, 7, 20) },
                new Lid { LidID = 4, Naam = "Sofie", GeboorteDatum = new DateTime(1992, 12, 1) }
            );

            modelBuilder.Entity<LidBoek>().HasData(
                new LidBoek { LidBoekID = 1, LidID = 1, ISBN = "9781402894626", UitleenDatum = new DateTime(2023, 1, 15), InleverDatum = new DateTime(2023, 2, 15) },
                new LidBoek { LidBoekID = 2, LidID = 2, ISBN = "9783161484100", UitleenDatum = new DateTime(2023, 1, 20), InleverDatum = new DateTime(2023, 2, 20) }
            );
        }
    }
}


