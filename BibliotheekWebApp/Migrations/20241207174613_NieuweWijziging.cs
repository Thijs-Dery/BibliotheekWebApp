using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BibliotheekWebApp.Migrations
{
    /// <inheritdoc />
    public partial class NieuweWijziging : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "2" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.UpdateData(
                table: "Boeken",
                keyColumn: "ISBN",
                keyValue: "9781402894626",
                column: "PublicatieDatum",
                value: new DateTime(2024, 12, 7, 18, 46, 12, 702, DateTimeKind.Local).AddTicks(7726));

            migrationBuilder.UpdateData(
                table: "Boeken",
                keyColumn: "ISBN",
                keyValue: "9783161484100",
                column: "PublicatieDatum",
                value: new DateTime(2024, 12, 7, 18, 46, 12, 702, DateTimeKind.Local).AddTicks(7792));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Achternaam", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "Voornaam" },
                values: new object[,]
                {
                    { "1", 0, "User", "77c0052b-50b9-4ffb-9eaa-da54afd1afbc", "admin@example.com", true, false, null, "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEA9urPnVA4wpFv4Wkg2Jm7JN7B6sPKilBmFaGOU1KRV7npZX8/dojV3e9pD7hEfChQ==", null, false, "19cb106f-f7c2-4273-ac01-cda8aaee759e", false, "admin@example.com", "Admin" },
                    { "2", 0, "User", "1f316895-9335-4daa-90cd-e7d1018e5aa7", "user@example.com", true, false, null, "USER@EXAMPLE.COM", "USER@EXAMPLE.COM", "AQAAAAIAAYagAAAAEOzfwdEjHZcZexIIL7PPu5jrTjQO2hpQCpfOVl3RJZO/0TGlHU0xla5CFcUNJRWrow==", null, false, "fc37ad37-f344-423a-ad72-cc3c349fffa4", false, "user@example.com", "Regular" }
                });

            migrationBuilder.UpdateData(
                table: "Boeken",
                keyColumn: "ISBN",
                keyValue: "9781402894626",
                column: "PublicatieDatum",
                value: new DateTime(2024, 12, 5, 23, 55, 25, 413, DateTimeKind.Local).AddTicks(5777));

            migrationBuilder.UpdateData(
                table: "Boeken",
                keyColumn: "ISBN",
                keyValue: "9783161484100",
                column: "PublicatieDatum",
                value: new DateTime(2024, 12, 5, 23, 55, 25, 413, DateTimeKind.Local).AddTicks(5879));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "1" },
                    { "2", "2" }
                });
        }
    }
}
