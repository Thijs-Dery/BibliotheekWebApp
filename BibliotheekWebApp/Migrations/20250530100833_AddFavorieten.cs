using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotheekWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddFavorieten : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Favorieten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GebruikerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AuteurID = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorieten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorieten_AspNetUsers_GebruikerId",
                        column: x => x.GebruikerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorieten_Auteurs_AuteurID",
                        column: x => x.AuteurID,
                        principalTable: "Auteurs",
                        principalColumn: "AuteurID");
                    table.ForeignKey(
                        name: "FK_Favorieten_Boeken_ISBN",
                        column: x => x.ISBN,
                        principalTable: "Boeken",
                        principalColumn: "ISBN");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favorieten_AuteurID",
                table: "Favorieten",
                column: "AuteurID");

            migrationBuilder.CreateIndex(
                name: "IX_Favorieten_GebruikerId",
                table: "Favorieten",
                column: "GebruikerId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorieten_ISBN",
                table: "Favorieten",
                column: "ISBN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favorieten");
        }
    }
}
