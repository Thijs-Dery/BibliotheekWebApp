using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BibliotheekWebApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auteurs",
                columns: table => new
                {
                    AuteurID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GeboorteDatum = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auteurs", x => x.AuteurID);
                });

            migrationBuilder.CreateTable(
                name: "Leden",
                columns: table => new
                {
                    LidID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GeboorteDatum = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leden", x => x.LidID);
                });

            migrationBuilder.CreateTable(
                name: "Boeken",
                columns: table => new
                {
                    ISBN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Titel = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicatieDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuteurID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boeken", x => x.ISBN);
                    table.ForeignKey(
                        name: "FK_Boeken_Auteurs_AuteurID",
                        column: x => x.AuteurID,
                        principalTable: "Auteurs",
                        principalColumn: "AuteurID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LidBoeken",
                columns: table => new
                {
                    LidBoekID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LidID = table.Column<int>(type: "int", nullable: true),
                    ISBN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UitleenDatum = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InleverDatum = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LidBoeken", x => x.LidBoekID);
                    table.ForeignKey(
                        name: "FK_LidBoeken_Boeken_ISBN",
                        column: x => x.ISBN,
                        principalTable: "Boeken",
                        principalColumn: "ISBN",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LidBoeken_Leden_LidID",
                        column: x => x.LidID,
                        principalTable: "Leden",
                        principalColumn: "LidID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Auteurs",
                columns: new[] { "AuteurID", "GeboorteDatum", "Naam" },
                values: new object[,]
                {
                    { 1, new DateTime(1975, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Auteur 1" },
                    { 2, new DateTime(1980, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Auteur 2" },
                    { 3, new DateTime(1995, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Auteur 3" },
                    { 4, new DateTime(1978, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Auteur 4" }
                });

            migrationBuilder.InsertData(
                table: "Leden",
                columns: new[] { "LidID", "GeboorteDatum", "Naam" },
                values: new object[,]
                {
                    { 1, new DateTime(1990, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Freddy" },
                    { 2, new DateTime(1985, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jochim" },
                    { 3, new DateTime(2000, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jos" },
                    { 4, new DateTime(1992, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sofie" }
                });

            migrationBuilder.InsertData(
                table: "Boeken",
                columns: new[] { "ISBN", "AuteurID", "Genre", "PublicatieDatum", "Titel" },
                values: new object[,]
                {
                    { "9781402894626", 1, "Koken", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Frieda Kroket" },
                    { "9783161484100", 2, "Koken", new DateTime(2021, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Koken met Henk" },
                    { "TEST-0001", 4, "Avontuur", new DateTime(2019, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "De Avonturen van Bob" },
                    { "TEST-010e1999", 3, "Fictie", new DateTime(2022, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wdsawd" }
                });

            migrationBuilder.InsertData(
                table: "LidBoeken",
                columns: new[] { "LidBoekID", "ISBN", "InleverDatum", "LidID", "UitleenDatum" },
                values: new object[,]
                {
                    { 1, "9781402894626", new DateTime(2023, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "9783161484100", new DateTime(2023, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boeken_AuteurID",
                table: "Boeken",
                column: "AuteurID");

            migrationBuilder.CreateIndex(
                name: "IX_LidBoeken_ISBN",
                table: "LidBoeken",
                column: "ISBN");

            migrationBuilder.CreateIndex(
                name: "IX_LidBoeken_LidID",
                table: "LidBoeken",
                column: "LidID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LidBoeken");

            migrationBuilder.DropTable(
                name: "Boeken");

            migrationBuilder.DropTable(
                name: "Leden");

            migrationBuilder.DropTable(
                name: "Auteurs");
        }
    }
}
