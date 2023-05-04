using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Apontei.Migrations
{
    public partial class Justificativa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Justificativas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnexarDocumento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    GestorId = table.Column<int>(type: "int", nullable: false),
                    FuncionarioId = table.Column<int>(type: "int", nullable: true),
                    JustificativaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Justificativas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Justificativas_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Justificativas_Funcionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Justificativas_Funcionarios_GestorId",
                        column: x => x.GestorId,
                        principalTable: "Funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Justificativas_Justificativas_JustificativaId",
                        column: x => x.JustificativaId,
                        principalTable: "Justificativas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JustificativaPonto",
                columns: table => new
                {
                    JustificativasId = table.Column<int>(type: "int", nullable: false),
                    PontosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JustificativaPonto", x => new { x.JustificativasId, x.PontosId });
                    table.ForeignKey(
                        name: "FK_JustificativaPonto_Justificativas_JustificativasId",
                        column: x => x.JustificativasId,
                        principalTable: "Justificativas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JustificativaPonto_Pontos_PontosId",
                        column: x => x.PontosId,
                        principalTable: "Pontos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JustificativaPonto_PontosId",
                table: "JustificativaPonto",
                column: "PontosId");

            migrationBuilder.CreateIndex(
                name: "IX_Justificativas_EmpresaId",
                table: "Justificativas",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Justificativas_FuncionarioId",
                table: "Justificativas",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Justificativas_GestorId",
                table: "Justificativas",
                column: "GestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Justificativas_JustificativaId",
                table: "Justificativas",
                column: "JustificativaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JustificativaPonto");

            migrationBuilder.DropTable(
                name: "Justificativas");
        }
    }
}
