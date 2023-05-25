using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace sistema_de_ponto.Migrations
{
    public partial class m05 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "TotalDeHoras",
                table: "RegistraPontos",
                type: "time",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalDeHoras",
                table: "RegistraPontos");
        }
    }
}
