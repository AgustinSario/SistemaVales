using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVales.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelsForWorkflowV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Receta",
                table: "Expedientes",
                newName: "TieneRecetaFisica");

            migrationBuilder.AddColumn<bool>(
                name: "EsAsistenciaComunidad",
                table: "Recetas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "MontoTotal",
                table: "Recetas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "CumpleFormularioTerapeutico",
                table: "Expedientes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ObservacionesFarmacia",
                table: "Expedientes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecetaId",
                table: "Expedientes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResultadoAuditoria",
                table: "Expedientes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expedientes_RecetaId",
                table: "Expedientes",
                column: "RecetaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expedientes_Recetas_RecetaId",
                table: "Expedientes",
                column: "RecetaId",
                principalTable: "Recetas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expedientes_Recetas_RecetaId",
                table: "Expedientes");

            migrationBuilder.DropIndex(
                name: "IX_Expedientes_RecetaId",
                table: "Expedientes");

            migrationBuilder.DropColumn(
                name: "EsAsistenciaComunidad",
                table: "Recetas");

            migrationBuilder.DropColumn(
                name: "MontoTotal",
                table: "Recetas");

            migrationBuilder.DropColumn(
                name: "CumpleFormularioTerapeutico",
                table: "Expedientes");

            migrationBuilder.DropColumn(
                name: "ObservacionesFarmacia",
                table: "Expedientes");

            migrationBuilder.DropColumn(
                name: "RecetaId",
                table: "Expedientes");

            migrationBuilder.DropColumn(
                name: "ResultadoAuditoria",
                table: "Expedientes");

            migrationBuilder.RenameColumn(
                name: "TieneRecetaFisica",
                table: "Expedientes",
                newName: "Receta");
        }
    }
}
