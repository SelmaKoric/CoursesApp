using Microsoft.EntityFrameworkCore.Migrations;

namespace ComfyLearn.Migrations
{
    public partial class clanak_like_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ClanakLike",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClanakLike_UserId",
                table: "ClanakLike",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClanakLike_AspNetUsers_UserId",
                table: "ClanakLike",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClanakLike_AspNetUsers_UserId",
                table: "ClanakLike");

            migrationBuilder.DropIndex(
                name: "IX_ClanakLike_UserId",
                table: "ClanakLike");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ClanakLike");
        }
    }
}
