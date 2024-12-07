using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotheekWebApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBoekSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Boeken",
                keyColumn: "ISBN",
                keyValue: "9781402894626",
                column: "PublicatieDatum",
                value: new DateTime(2024, 12, 7, 19, 2, 25, 844, DateTimeKind.Local).AddTicks(6647));

            migrationBuilder.UpdateData(
                table: "Boeken",
                keyColumn: "ISBN",
                keyValue: "9783161484100",
                column: "PublicatieDatum",
                value: new DateTime(2024, 12, 7, 19, 2, 25, 844, DateTimeKind.Local).AddTicks(6711));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
