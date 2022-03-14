using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComfyLearn.Migrations
{
    public partial class clanak_likes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Clanak");

            migrationBuilder.CreateTable(
                name: "ClanakLike",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClanakId = table.Column<int>(nullable: false),
                    IpAddress = table.Column<string>(nullable: true),
                    Like = table.Column<bool>(nullable: false),
                    Datum = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClanakLike", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClanakLike_Clanak_ClanakId",
                        column: x => x.ClanakId,
                        principalTable: "Clanak",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClanakLike_ClanakId",
                table: "ClanakLike",
                column: "ClanakId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClanakLike");

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Clanak",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
