using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVales.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddHospitalEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Create Hospitales table first
            migrationBuilder.CreateTable(
                name: "Hospitales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ciudad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitales", x => x.Id);
                });

            // 2. Insert a default hospital so existing pacientes can reference it
            migrationBuilder.Sql(
                "SET IDENTITY_INSERT [Hospitales] ON; " +
                "INSERT INTO [Hospitales] ([Id], [Nombre], [Direccion], [Telefono], [Ciudad]) " +
                "VALUES (1, N'Sin Asignar', N'', N'', N''); " +
                "SET IDENTITY_INSERT [Hospitales] OFF;");

            // 3. Drop old string column
            migrationBuilder.DropColumn(
                name: "HospitalAtencion",
                table: "Pacientes");

            // 4. Add HospitalId column with default = 1 (the hospital we just created)
            migrationBuilder.AddColumn<int>(
                name: "HospitalId",
                table: "Pacientes",
                type: "int",
                nullable: false,
                defaultValue: 1);

            // 5. Update existing rows to reference the default hospital
            migrationBuilder.Sql("UPDATE [Pacientes] SET [HospitalId] = 1 WHERE [HospitalId] = 0;");

            // 6. Create index and FK
            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_HospitalId",
                table: "Pacientes",
                column: "HospitalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_Hospitales_HospitalId",
                table: "Pacientes",
                column: "HospitalId",
                principalTable: "Hospitales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_Hospitales_HospitalId",
                table: "Pacientes");

            migrationBuilder.DropTable(
                name: "Hospitales");

            migrationBuilder.DropIndex(
                name: "IX_Pacientes_HospitalId",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "HospitalId",
                table: "Pacientes");

            migrationBuilder.AddColumn<string>(
                name: "HospitalAtencion",
                table: "Pacientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
