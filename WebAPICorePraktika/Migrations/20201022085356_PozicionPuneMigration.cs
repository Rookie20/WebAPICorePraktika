using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPICorePraktika.Migrations
{
    public partial class PozicionPuneMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PozicioniPune",
                columns: table => new
                {
                    PozicionPuneId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PozicionPuneEmri = table.Column<string>(nullable: false),
                    DepartamentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PozicioniPune", x => x.PozicionPuneId);
                    table.ForeignKey(
                        name: "FK_PozicioniPune_Departament_DepartamentId",
                        column: x => x.DepartamentId,
                        principalTable: "Departament",
                        principalColumn: "DepartamentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PozicioniPune_DepartamentId",
                table: "PozicioniPune",
                column: "DepartamentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PozicioniPune");
        }
    }
}
