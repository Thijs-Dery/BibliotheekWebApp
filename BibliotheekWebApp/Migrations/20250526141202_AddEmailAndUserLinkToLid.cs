using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotheekWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailAndUserLinkToLid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Leden",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "Leden",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Leden");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Leden");
        }
    }
}
