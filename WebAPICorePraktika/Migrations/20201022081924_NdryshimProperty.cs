using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPICorePraktika.Migrations
{
    public partial class NdryshimProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DepDataKrijimit",
                table: "Departament",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DepDataKrijimit",
                table: "Departament",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
