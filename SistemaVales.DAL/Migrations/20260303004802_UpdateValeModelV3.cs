using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVales.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateValeModelV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EntregadoPor",
                table: "Vales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedicamentoId",
                table: "Vales",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroVale",
                table: "Vales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Vales_MedicamentoId",
                table: "Vales",
                column: "MedicamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vales_Medicamentos_MedicamentoId",
                table: "Vales",
                column: "MedicamentoId",
                principalTable: "Medicamentos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vales_Medicamentos_MedicamentoId",
                table: "Vales");

            migrationBuilder.DropIndex(
                name: "IX_Vales_MedicamentoId",
                table: "Vales");

            migrationBuilder.DropColumn(
                name: "EntregadoPor",
                table: "Vales");

            migrationBuilder.DropColumn(
                name: "MedicamentoId",
                table: "Vales");

            migrationBuilder.DropColumn(
                name: "NumeroVale",
                table: "Vales");
        }
    }
}
