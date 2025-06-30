using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace contratosimples_api.Migrations.ContratoSimplesDb
{
    /// <inheritdoc />
    public partial class TabelasAditivo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aditivo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContratoId = table.Column<int>(type: "integer", nullable: false),
                    Prazo = table.Column<int>(type: "integer", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aditivo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aditivo_Contrato_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contrato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AditivoItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AditivoId = table.Column<int>(type: "integer", nullable: false),
                    ContratoItemId = table.Column<int>(type: "integer", nullable: false),
                    TipoAditivo = table.Column<int>(type: "integer", nullable: false),
                    Quantidade = table.Column<decimal>(type: "numeric", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AditivoItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AditivoItem_Aditivo_AditivoId",
                        column: x => x.AditivoId,
                        principalTable: "Aditivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AditivoItem_ContratoItem_ContratoItemId",
                        column: x => x.ContratoItemId,
                        principalTable: "ContratoItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aditivo_ContratoId",
                table: "Aditivo",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_AditivoItem_AditivoId",
                table: "AditivoItem",
                column: "AditivoId");

            migrationBuilder.CreateIndex(
                name: "IX_AditivoItem_ContratoItemId",
                table: "AditivoItem",
                column: "ContratoItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AditivoItem");

            migrationBuilder.DropTable(
                name: "Aditivo");
        }
    }
}
