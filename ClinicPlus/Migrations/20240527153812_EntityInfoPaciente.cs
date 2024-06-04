using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicPlus.Migrations
{
    /// <inheritdoc />
    public partial class EntityInfoPaciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InformacoesComplementaresPaciente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Alergias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicamentosEmUso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CirurgiasRealizadas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdPaciente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformacoesComplementaresPaciente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InformacoesComplementaresPaciente_Pacientes_IdPaciente",
                        column: x => x.IdPaciente,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InformacoesComplementaresPaciente_IdPaciente",
                table: "InformacoesComplementaresPaciente",
                column: "IdPaciente",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InformacoesComplementaresPaciente");
        }
    }
}
