using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPICorePraktika.Migrations
{
    public partial class Historiku : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistorikuPoziPunes",
                columns: table => new
                {
                    HistorikuId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PozicioniPerpara = table.Column<string>(nullable: true),
                    PozicioniPas = table.Column<string>(nullable: true),
                    Id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorikuPoziPunes", x => x.HistorikuId);
                    table.ForeignKey(
                        name: "FK_HistorikuPoziPunes_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistorikuPoziPunes_Id",
                table: "HistorikuPoziPunes",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistorikuPoziPunes");
        }
    }
}
