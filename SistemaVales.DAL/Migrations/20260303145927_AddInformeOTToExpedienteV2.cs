using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVales.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddInformeOTToExpedienteV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InformeOT",
                table: "Expedientes",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InformeOT",
                table: "Expedientes");
        }
    }
}
