using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BibliotheekWebApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                    Naam = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    GeboorteDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    Naam = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    GeboorteDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicatieDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AuteurID = table.Column<int>(type: "int", nullable: false)
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
                    LidID = table.Column<int>(type: "int", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UitleenDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InleverDatum = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                columns: new[] { "AuteurID", "GeboorteDatum", "IsDeleted", "Naam" },
                values: new object[,]
                {
                    { 1, new DateTime(1975, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Auteur 1" },
                    { 2, new DateTime(1980, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Auteur 2" }
                });

            migrationBuilder.InsertData(
                table: "Leden",
                columns: new[] { "LidID", "GeboorteDatum", "IsDeleted", "Naam" },
                values: new object[,]
                {
                    { 1, new DateTime(1990, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Freddy" },
                    { 2, new DateTime(1985, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Jochim" }
                });

            migrationBuilder.InsertData(
                table: "Boeken",
                columns: new[] { "ISBN", "AuteurID", "Genre", "IsDeleted", "PublicatieDatum", "Titel" },
                values: new object[,]
                {
                    { "9781402894626", 1, "Koken", false, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Frieda Kroket" },
                    { "9783161484100", 2, "Koken", false, new DateTime(2021, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Koken met Henk" }
                });

            migrationBuilder.InsertData(
                table: "LidBoeken",
                columns: new[] { "LidBoekID", "ISBN", "InleverDatum", "LidID", "UitleenDatum" },
                values: new object[] { 1, "9781402894626", new DateTime(2023, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

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
