using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPICorePraktika.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departament",
                columns: table => new
                {
                    DepartamentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartamentEmer = table.Column<string>(nullable: false),
                    DepDataKrijimit = table.Column<DateTime>(nullable: false),
                    DepartamentPershkrimi = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departament", x => x.DepartamentId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departament");
        }
    }
}
