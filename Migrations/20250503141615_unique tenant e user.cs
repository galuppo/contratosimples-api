using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace contratosimples_api.Migrations
{
    /// <inheritdoc />
    public partial class uniquetenanteuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tenant_Cpf_cnpj",
                table: "Tenant",
                column: "Cpf_cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tenant_Cpf_cnpj",
                table: "Tenant");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers");
        }
    }
}
