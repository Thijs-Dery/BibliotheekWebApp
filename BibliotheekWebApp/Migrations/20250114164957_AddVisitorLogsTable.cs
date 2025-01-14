using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BibliotheekWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddVisitorLogsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "Boeken",
                keyColumn: "ISBN",
                keyValue: "9781402894626");

            migrationBuilder.DeleteData(
                table: "Boeken",
                keyColumn: "ISBN",
                keyValue: "9783161484100");

            migrationBuilder.DeleteData(
                table: "Auteurs",
                keyColumn: "AuteurID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Auteurs",
                keyColumn: "AuteurID",
                keyValue: 2);

            migrationBuilder.CreateTable(
                name: "VisitorLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitorLogs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisitorLogs");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "Admin", "ADMIN" },
                    { "2", null, "User", "USER" }
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
                table: "Boeken",
                columns: new[] { "ISBN", "AuteurID", "Genre", "IsDeleted", "PublicatieDatum", "Titel" },
                values: new object[,]
                {
                    { "9781402894626", 1, "Koken", false, new DateTime(2024, 12, 7, 19, 2, 25, 844, DateTimeKind.Local).AddTicks(6647), "Frieda Kroket" },
                    { "9783161484100", 2, "Koken", false, new DateTime(2024, 12, 7, 19, 2, 25, 844, DateTimeKind.Local).AddTicks(6711), "Koken met Henk" }
                });
        }
    }
}
