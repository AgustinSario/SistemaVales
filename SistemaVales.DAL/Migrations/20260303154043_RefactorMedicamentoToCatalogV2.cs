using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVales.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RefactorMedicamentoToCatalogV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "IX_Medicamentos_ExpedienteId",
                table: "Medicamentos");

            migrationBuilder.DropIndex(
                name: "IX_Medicamentos_HospitalId",
                table: "Medicamentos");

            migrationBuilder.DropIndex(
                name: "IX_Medicamentos_PacienteId",
                table: "Medicamentos");

            migrationBuilder.DropColumn(
                name: "ExpedienteId",
                table: "Medicamentos");

            migrationBuilder.DropColumn(
                name: "HospitalId",
                table: "Medicamentos");

            migrationBuilder.DropColumn(
                name: "PacienteId",
                table: "Medicamentos");

            migrationBuilder.RenameColumn(
                name: "Presentacion",
                table: "Medicamentos",
                newName: "Unidad");

            migrationBuilder.RenameColumn(
                name: "NombreDroga",
                table: "Medicamentos",
                newName: "Nombre");

            migrationBuilder.AddColumn<string>(
                name: "Cantidad",
                table: "Medicamentos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Medicamentos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "Medicamentos");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Medicamentos");

            migrationBuilder.RenameColumn(
                name: "Unidad",
                table: "Medicamentos",
                newName: "Presentacion");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Medicamentos",
                newName: "NombreDroga");

            migrationBuilder.AddColumn<int>(
                name: "ExpedienteId",
                table: "Medicamentos",
                type: "int",
                nullable: true);

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
                name: "IX_Medicamentos_ExpedienteId",
                table: "Medicamentos",
                column: "ExpedienteId");

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
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicamentos_Pacientes_PacienteId",
                table: "Medicamentos",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "Id");
        }
    }
}
