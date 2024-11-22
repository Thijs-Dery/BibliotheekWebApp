﻿// <auto-generated />
using System;
using BibliotheekApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BibliotheekWebApp.Migrations
{
    [DbContext(typeof(BibliotheekContext))]
    partial class BibliotheekContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BibliotheekApp.Models.Auteur", b =>
                {
                    b.Property<int>("AuteurID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AuteurID"));

                    b.Property<DateTime>("GeboorteDatum")
                        .HasColumnType("datetime2");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("AuteurID");

                    b.ToTable("Auteurs");

                    b.HasData(
                        new
                        {
                            AuteurID = 1,
                            GeboorteDatum = new DateTime(1975, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Naam = "Auteur 1"
                        },
                        new
                        {
                            AuteurID = 2,
                            GeboorteDatum = new DateTime(1980, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Naam = "Auteur 2"
                        },
                        new
                        {
                            AuteurID = 3,
                            GeboorteDatum = new DateTime(1995, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Naam = "Auteur 3"
                        },
                        new
                        {
                            AuteurID = 4,
                            GeboorteDatum = new DateTime(1978, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Naam = "Auteur 4"
                        });
                });

            modelBuilder.Entity("BibliotheekApp.Models.Boek", b =>
                {
                    b.Property<string>("ISBN")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("AuteurID")
                        .HasColumnType("int");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PublicatieDatum")
                        .HasColumnType("datetime2");

                    b.Property<string>("Titel")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("ISBN");

                    b.HasIndex("AuteurID");

                    b.ToTable("Boeken");

                    b.HasData(
                        new
                        {
                            ISBN = "9781402894626",
                            AuteurID = 1,
                            Genre = "Koken",
                            PublicatieDatum = new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Titel = "Frieda Kroket"
                        },
                        new
                        {
                            ISBN = "9783161484100",
                            AuteurID = 2,
                            Genre = "Koken",
                            PublicatieDatum = new DateTime(2021, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Titel = "Koken met Henk"
                        },
                        new
                        {
                            ISBN = "TEST-010e1999",
                            AuteurID = 3,
                            Genre = "Fictie",
                            PublicatieDatum = new DateTime(2022, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Titel = "Wdsawd"
                        },
                        new
                        {
                            ISBN = "TEST-0001",
                            AuteurID = 4,
                            Genre = "Avontuur",
                            PublicatieDatum = new DateTime(2019, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Titel = "De Avonturen van Bob"
                        });
                });

            modelBuilder.Entity("BibliotheekApp.Models.Lid", b =>
                {
                    b.Property<int>("LidID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LidID"));

                    b.Property<DateTime>("GeboorteDatum")
                        .HasColumnType("datetime2");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("LidID");

                    b.ToTable("Leden");

                    b.HasData(
                        new
                        {
                            LidID = 1,
                            GeboorteDatum = new DateTime(1990, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Naam = "Freddy"
                        },
                        new
                        {
                            LidID = 2,
                            GeboorteDatum = new DateTime(1985, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Naam = "Jochim"
                        },
                        new
                        {
                            LidID = 3,
                            GeboorteDatum = new DateTime(2000, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Naam = "Jos"
                        },
                        new
                        {
                            LidID = 4,
                            GeboorteDatum = new DateTime(1992, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Naam = "Sofie"
                        });
                });

            modelBuilder.Entity("BibliotheekApp.Models.LidBoek", b =>
                {
                    b.Property<int>("LidBoekID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LidBoekID"));

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("ISBN");

                    b.Property<DateTime?>("InleverDatum")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LidID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UitleenDatum")
                        .HasColumnType("datetime2");

                    b.HasKey("LidBoekID");

                    b.HasIndex("ISBN");

                    b.HasIndex("LidID");

                    b.ToTable("LidBoeken");

                    b.HasData(
                        new
                        {
                            LidBoekID = 1,
                            ISBN = "9781402894626",
                            InleverDatum = new DateTime(2023, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LidID = 1,
                            UitleenDatum = new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            LidBoekID = 2,
                            ISBN = "9783161484100",
                            InleverDatum = new DateTime(2023, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LidID = 2,
                            UitleenDatum = new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("BibliotheekApp.Models.Boek", b =>
                {
                    b.HasOne("BibliotheekApp.Models.Auteur", "Auteur")
                        .WithMany("Boeken")
                        .HasForeignKey("AuteurID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Auteur");
                });

            modelBuilder.Entity("BibliotheekApp.Models.LidBoek", b =>
                {
                    b.HasOne("BibliotheekApp.Models.Boek", "Boek")
                        .WithMany("LidBoeken")
                        .HasForeignKey("ISBN")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BibliotheekApp.Models.Lid", "Lid")
                        .WithMany("GeleendeBoeken")
                        .HasForeignKey("LidID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Boek");

                    b.Navigation("Lid");
                });

            modelBuilder.Entity("BibliotheekApp.Models.Auteur", b =>
                {
                    b.Navigation("Boeken");
                });

            modelBuilder.Entity("BibliotheekApp.Models.Boek", b =>
                {
                    b.Navigation("LidBoeken");
                });

            modelBuilder.Entity("BibliotheekApp.Models.Lid", b =>
                {
                    b.Navigation("GeleendeBoeken");
                });
#pragma warning restore 612, 618
        }
    }
}