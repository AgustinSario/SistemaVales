using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVales.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMedicamentoAssociationsV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicamentos_Expedientes_ExpedienteId",
                table: "Medicamentos");

            migrationBuilder.AlterColumn<int>(
                name: "ExpedienteId",
                table: "Medicamentos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "HospitalId",
                table: "Medicamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PacienteId",
                table: "Medicamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Medicamentos_HospitalId",
                table: "Medicamentos",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicamentos_PacienteId",
                table: "Medicamentos",
                column: "PacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicamentos_Expedientes_ExpedienteId",
                table: "Medicamentos",
                column: "ExpedienteId",
                principalTable: "Expedientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicamentos_Hospitales_HospitalId",
                table: "Medicamentos",
                column: "HospitalId",
                principalTable: "Hospitales",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicamentos_Pacientes_PacienteId",
                table: "Medicamentos",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicamentos_Expedientes_ExpedienteId",
                table: "Medicamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicamentos_Hospitales_HospitalId",
                table: "Medicamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicamentos_Pacientes_PacienteId",
                table: "Medicamentos");

            migrationBuilder.DropIndex(
                name: "IX_Medicamentos_HospitalId",
                table: "Medicamentos");

            migrationBuilder.DropIndex(
                name: "IX_Medicamentos_PacienteId",
                table: "Medicamentos");

            migrationBuilder.DropColumn(
                name: "HospitalId",
                table: "Medicamentos");

            migrationBuilder.DropColumn(
                name: "PacienteId",
                table: "Medicamentos");

            migrationBuilder.AlterColumn<int>(
                name: "ExpedienteId",
                table: "Medicamentos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicamentos_Expedientes_ExpedienteId",
                table: "Medicamentos",
                column: "ExpedienteId",
                principalTable: "Expedientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
