using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotheekWebApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Boeken",
                keyColumn: "ISBN",
                keyValue: "9781402894626",
                column: "PublicatieDatum",
                value: new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Boeken",
                keyColumn: "ISBN",
                keyValue: "9783161484100",
                column: "PublicatieDatum",
                value: new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Boeken",
                keyColumn: "ISBN",
                keyValue: "9781402894626",
                column: "PublicatieDatum",
                value: new DateTime(2025, 1, 21, 20, 46, 27, 90, DateTimeKind.Local).AddTicks(1947));

            migrationBuilder.UpdateData(
                table: "Boeken",
                keyColumn: "ISBN",
                keyValue: "9783161484100",
                column: "PublicatieDatum",
                value: new DateTime(2025, 1, 21, 20, 46, 27, 90, DateTimeKind.Local).AddTicks(2015));
        }
    }
}
