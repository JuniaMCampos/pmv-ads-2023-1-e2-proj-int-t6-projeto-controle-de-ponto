using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Apontei.Migrations
{
    public partial class Ponto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pontos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraEntrada = table.Column<TimeSpan>(type: "time", nullable: false),
                    HoraIntervaloInicial = table.Column<TimeSpan>(type: "time", nullable: false),
                    HoraIntervaloFinal = table.Column<TimeSpan>(type: "time", nullable: false),
                    HoraSaida = table.Column<TimeSpan>(type: "time", nullable: false),
                    HorasExtras = table.Column<TimeSpan>(type: "time", nullable: false),
                    Turno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FuncionarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pontos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pontos_Funcionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pontos_FuncionarioId",
                table: "Pontos",
                column: "FuncionarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pontos");
        }
    }
}
