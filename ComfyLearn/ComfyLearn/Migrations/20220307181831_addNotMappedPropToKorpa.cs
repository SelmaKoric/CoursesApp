using Microsoft.EntityFrameworkCore.Migrations;

namespace ComfyLearn.Migrations
{
    public partial class addNotMappedPropToKorpa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Korpa_AspNetUsers_userId",
                table: "Korpa");

            migrationBuilder.DropIndex(
                name: "IX_Korpa_userId",
                table: "Korpa");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "Korpa",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "Korpa",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Korpa_userId",
                table: "Korpa",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Korpa_AspNetUsers_userId",
                table: "Korpa",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
