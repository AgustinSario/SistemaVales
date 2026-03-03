using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVales.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadeMedicamentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicamentos_Hospitales_HospitalId",
                table: "Medicamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicamentos_Pacientes_PacienteId",
                table: "Medicamentos");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicamentos_Hospitales_HospitalId",
                table: "Medicamentos",
                column: "HospitalId",
                principalTable: "Hospitales",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicamentos_Pacientes_PacienteId",
                table: "Medicamentos",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicamentos_Hospitales_HospitalId",
                table: "Medicamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicamentos_Pacientes_PacienteId",
                table: "Medicamentos");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicamentos_Hospitales_HospitalId",
                table: "Medicamentos",
                column: "HospitalId",
                principalTable: "Hospitales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicamentos_Pacientes_PacienteId",
                table: "Medicamentos",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
