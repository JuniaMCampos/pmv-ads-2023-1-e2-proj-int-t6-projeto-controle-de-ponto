using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace sistema_de_ponto.Migrations
{
    public partial class m02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

             migrationBuilder.DropColumn(
                name: "HoraEntrada",
                table: "Ponto");

            migrationBuilder.DropColumn(
                name: "HoraExtra",
                table: "Ponto");

            migrationBuilder.DropColumn(
                name: "HoraIntervaloFinal",
                table: "Ponto");

            migrationBuilder.DropColumn(
                name: "HoraIntervaloInicial",
                table: "Ponto");

            migrationBuilder.DropColumn(
                name: "HoraSaida",
                table: "Ponto");

           
            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Ponto",
                newName: "HoraSaida2");

            migrationBuilder.AddColumn<DateTime>(
                name: "HoraEntrada1",
                table: "Ponto",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "HoraEntrada2",
                table: "Ponto",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "HoraSaida1",
                table: "Ponto",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoraEntrada1",
                table: "Ponto");

            migrationBuilder.DropColumn(
                name: "HoraEntrada2",
                table: "Ponto");

            migrationBuilder.DropColumn(
                name: "HoraSaida1",
                table: "Ponto");

            migrationBuilder.RenameColumn(
                name: "HoraSaida2",
                table: "Ponto",
                newName: "Data");

           
             

           
        }
    }
}
