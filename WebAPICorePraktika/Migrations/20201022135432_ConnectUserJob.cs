using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPICorePraktika.Migrations
{
    public partial class ConnectUserJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PozicionPuneId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PozicionPuneId",
                table: "AspNetUsers",
                column: "PozicionPuneId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PozicioniPune_PozicionPuneId",
                table: "AspNetUsers",
                column: "PozicionPuneId",
                principalTable: "PozicioniPune",
                principalColumn: "PozicionPuneId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PozicioniPune_PozicionPuneId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PozicionPuneId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PozicionPuneId",
                table: "AspNetUsers");
        }
    }
}
