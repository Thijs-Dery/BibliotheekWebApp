﻿using BibliotheekApp.Data;
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

            // Roep de SeedData klasse aan voor seeding
            SeedData.Seed(modelBuilder);
        }
    }
}
