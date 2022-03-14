using Microsoft.EntityFrameworkCore.Migrations;

namespace ComfyLearn.Migrations
{
    public partial class removeLikesDislikesFromComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dislikes",
                table: "BlogKomentari");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "BlogKomentari");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Dislikes",
                table: "BlogKomentari",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "BlogKomentari",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
