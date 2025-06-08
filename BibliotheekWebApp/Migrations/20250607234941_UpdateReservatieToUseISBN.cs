using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotheekWebApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReservatieToUseISBN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoekId",
                table: "Reservaties");

            migrationBuilder.AddColumn<string>(
                name: "ISBN",
                table: "Reservaties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ISBN",
                table: "Reservaties");

            migrationBuilder.AddColumn<int>(
                name: "BoekId",
                table: "Reservaties",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
