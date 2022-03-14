using Microsoft.EntityFrameworkCore.Migrations;

namespace ComfyLearn.Migrations
{
    public partial class changeKupljenaKorpaModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KupljenaKorpa_AspNetUsers_UserId",
                table: "KupljenaKorpa");

            migrationBuilder.DropIndex(
                name: "IX_KupljenaKorpa_UserId",
                table: "KupljenaKorpa");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "KupljenaKorpa");

            migrationBuilder.AlterColumn<string>(
                name: "KorisnikId",
                table: "KupljenaKorpa",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_KupljenaKorpa_KorisnikId",
                table: "KupljenaKorpa",
                column: "KorisnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_KupljenaKorpa_AspNetUsers_KorisnikId",
                table: "KupljenaKorpa",
                column: "KorisnikId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KupljenaKorpa_AspNetUsers_KorisnikId",
                table: "KupljenaKorpa");

            migrationBuilder.DropIndex(
                name: "IX_KupljenaKorpa_KorisnikId",
                table: "KupljenaKorpa");

            migrationBuilder.AlterColumn<string>(
                name: "KorisnikId",
                table: "KupljenaKorpa",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "KupljenaKorpa",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_KupljenaKorpa_UserId",
                table: "KupljenaKorpa",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_KupljenaKorpa_AspNetUsers_UserId",
                table: "KupljenaKorpa",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
