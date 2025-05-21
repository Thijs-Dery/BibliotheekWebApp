using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotheekWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddReservatie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boeken_Auteurs_AuteurID",
                table: "Boeken");

            migrationBuilder.DropForeignKey(
                name: "FK_LidBoeken_Boeken_ISBN",
                table: "LidBoeken");

            migrationBuilder.CreateTable(
                name: "Reservaties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GebruikerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoekId = table.Column<int>(type: "int", nullable: false),
                    ReservatieDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Verwerkt = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservaties", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Boeken_Auteurs_AuteurID",
                table: "Boeken",
                column: "AuteurID",
                principalTable: "Auteurs",
                principalColumn: "AuteurID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LidBoeken_Boeken_ISBN",
                table: "LidBoeken",
                column: "ISBN",
                principalTable: "Boeken",
                principalColumn: "ISBN",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boeken_Auteurs_AuteurID",
                table: "Boeken");

            migrationBuilder.DropForeignKey(
                name: "FK_LidBoeken_Boeken_ISBN",
                table: "LidBoeken");

            migrationBuilder.DropTable(
                name: "Reservaties");

            migrationBuilder.AddForeignKey(
                name: "FK_Boeken_Auteurs_AuteurID",
                table: "Boeken",
                column: "AuteurID",
                principalTable: "Auteurs",
                principalColumn: "AuteurID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LidBoeken_Boeken_ISBN",
                table: "LidBoeken",
                column: "ISBN",
                principalTable: "Boeken",
                principalColumn: "ISBN",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
