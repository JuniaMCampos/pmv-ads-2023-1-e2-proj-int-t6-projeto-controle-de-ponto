using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace sistema_de_ponto.Migrations
{
    public partial class m06 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaldoDeHoras",
                table: "RegistraPontos");

            migrationBuilder.DropColumn(
                name: "TotalDeHoras",
                table: "RegistraPontos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "SaldoDeHoras",
                table: "RegistraPontos",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TotalDeHoras",
                table: "RegistraPontos",
                type: "time",
                nullable: true);
        }
    }
}
