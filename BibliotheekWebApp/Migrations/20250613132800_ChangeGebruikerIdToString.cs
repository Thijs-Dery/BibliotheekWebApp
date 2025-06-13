using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotheekWebApp.Migrations
{
    /// <inheritdoc />
    public partial class ChangeGebruikerIdToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorieten_Leden_GebruikerId",
                table: "Favorieten");

            migrationBuilder.AlterColumn<string>(
                name: "GebruikerId",
                table: "Favorieten",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorieten_AspNetUsers_GebruikerId",
                table: "Favorieten",
                column: "GebruikerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorieten_AspNetUsers_GebruikerId",
                table: "Favorieten");

            migrationBuilder.AlterColumn<int>(
                name: "GebruikerId",
                table: "Favorieten",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorieten_Leden_GebruikerId",
                table: "Favorieten",
                column: "GebruikerId",
                principalTable: "Leden",
                principalColumn: "LidID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
