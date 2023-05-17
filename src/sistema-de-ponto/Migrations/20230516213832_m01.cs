using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace sistema_de_ponto.Migrations
{
    public partial class m01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
         
            migrationBuilder.CreateTable(
                name: "RegistraPontos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FuncionarioId = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraEntrada1 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HoraSaida1 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HoraEntrada2 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HoraSaida2 = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistraPontos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistraPontos_Funcionario_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            

            migrationBuilder.CreateIndex(
                name: "IX_RegistraPontos_FuncionarioId",
                table: "RegistraPontos",
                column: "FuncionarioId");

                      
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
                      
            migrationBuilder.DropTable(
                name: "RegistraPontos");

         

           

           
        }
    }
}
