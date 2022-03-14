using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComfyLearn.Migrations
{
    public partial class addKupljenaKorpaModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KorpaDetalji");

            migrationBuilder.DropTable(
                name: "KorpaHeader");

            migrationBuilder.CreateTable(
                name: "KupljenaKorpa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    DatumKupovine = table.Column<DateTime>(nullable: false),
                    UkupanIznos = table.Column<double>(nullable: false),
                    NazivKupona = table.Column<string>(nullable: true),
                    KuponDiscount = table.Column<double>(nullable: false),
                    TransakcijaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KupljenaKorpa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KupljenaKorpa_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KupljenaKorpa_UserId",
                table: "KupljenaKorpa",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KupljenaKorpa");

            migrationBuilder.CreateTable(
                name: "KorpaHeader",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumKupovine = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KorisnikId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KuponDiscount = table.Column<double>(type: "float", nullable: false),
                    NazivKupona = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusPlacanja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransakcijaId = table.Column<int>(type: "int", nullable: false),
                    UkupanIznos = table.Column<double>(type: "float", nullable: false),
                    UkupanIznosPočetni = table.Column<double>(type: "float", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorpaHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KorpaHeader_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KorpaDetalji",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cijena = table.Column<double>(type: "float", nullable: false),
                    KupovinaHeaderId = table.Column<int>(type: "int", nullable: false),
                    KursId = table.Column<int>(type: "int", nullable: false),
                    NazivKursa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpisKursa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorpaDetalji", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KorpaDetalji_KorpaHeader_KupovinaHeaderId",
                        column: x => x.KupovinaHeaderId,
                        principalTable: "KorpaHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KorpaDetalji_Course_KursId",
                        column: x => x.KursId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KorpaDetalji_KupovinaHeaderId",
                table: "KorpaDetalji",
                column: "KupovinaHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_KorpaDetalji_KursId",
                table: "KorpaDetalji",
                column: "KursId");

            migrationBuilder.CreateIndex(
                name: "IX_KorpaHeader_UserId",
                table: "KorpaHeader",
                column: "UserId");
        }
    }
}
