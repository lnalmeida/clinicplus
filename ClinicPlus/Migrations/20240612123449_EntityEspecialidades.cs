using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicPlus.Migrations
{
    /// <inheritdoc />
    public partial class EntityEspecialidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Saturacao",
                table: "MonitoramentosPacientes",
                type: "TINYINT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "TYNYINT");

            migrationBuilder.AlterColumn<byte>(
                name: "FrequenciaCardiaca",
                table: "MonitoramentosPacientes",
                type: "TINYINT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "TYNYINT");

            migrationBuilder.AddColumn<int>(
                name: "EspecialidadeId",
                table: "Medicos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Especialidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Cor = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialidades", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medicos_EspecialidadeId",
                table: "Medicos",
                column: "EspecialidadeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicos_Especialidades_EspecialidadeId",
                table: "Medicos",
                column: "EspecialidadeId",
                principalTable: "Especialidades",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicos_Especialidades_EspecialidadeId",
                table: "Medicos");

            migrationBuilder.DropTable(
                name: "Especialidades");

            migrationBuilder.DropIndex(
                name: "IX_Medicos_EspecialidadeId",
                table: "Medicos");

            migrationBuilder.DropColumn(
                name: "EspecialidadeId",
                table: "Medicos");

            migrationBuilder.AlterColumn<int>(
                name: "Saturacao",
                table: "MonitoramentosPacientes",
                type: "TYNYINT",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "TINYINT");

            migrationBuilder.AlterColumn<int>(
                name: "FrequenciaCardiaca",
                table: "MonitoramentosPacientes",
                type: "TYNYINT",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "TINYINT");
        }
    }
}
