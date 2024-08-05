using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIBanco.InfraEstrutura.Migrations
{
    /// <inheritdoc />
    public partial class cliente_index_guid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Key",
                table: "Cliente",
                column: "Key",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cliente_Key",
                table: "Cliente");
        }
    }
}
