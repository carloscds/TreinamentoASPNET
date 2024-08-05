using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIBanco.InfraEstrutura.Migrations
{
    /// <inheritdoc />
    public partial class cliente_guid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Key",
                table: "Cliente",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                table: "Cliente");
        }
    }
}
