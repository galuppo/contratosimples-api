using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace contratosimples_api.Migrations.ContratoSimplesDb
{
    /// <inheritdoc />
    public partial class medicao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medicao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContratoId = table.Column<int>(type: "integer", nullable: false),
                    Data = table.Column<DateOnly>(type: "date", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicao_Contrato_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contrato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "MedicaoItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MedicaoId = table.Column<int>(type: "integer", nullable: false),
                    ItemContratoId = table.Column<int>(type: "integer", nullable: false),
                    Quantidade = table.Column<decimal>(type: "numeric", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicaoItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicaoItem_ContratoItem_ItemContratoId",
                        column: x => x.ItemContratoId,
                        principalTable: "ContratoItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_MedicaoItem_Medicao_MedicaoId",
                        column: x => x.MedicaoId,
                        principalTable: "Medicao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medicao_ContratoId",
                table: "Medicao",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicaoItem_ItemContratoId",
                table: "MedicaoItem",
                column: "ItemContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicaoItem_MedicaoId",
                table: "MedicaoItem",
                column: "MedicaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicaoItem");

            migrationBuilder.DropTable(
                name: "Medicao");
        }
    }
}
