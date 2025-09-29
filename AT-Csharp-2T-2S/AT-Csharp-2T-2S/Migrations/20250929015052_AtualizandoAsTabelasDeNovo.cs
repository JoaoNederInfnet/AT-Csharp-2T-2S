using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AT_Csharp_2T_2S.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoAsTabelasDeNovo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "AtualizadoEm", "CriadoEm", "Email", "Nome" },
                values: new object[] { 1L, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), "balalau@email.com", "Balalau da Silva" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
