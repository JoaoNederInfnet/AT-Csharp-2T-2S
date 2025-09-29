using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AT_Csharp_2T_2S.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoAsTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EstaDeletado",
                table: "PacotesTuristicos",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "PacotesTuristicos",
                keyColumn: "Id",
                keyValue: 1L,
                column: "EstaDeletado",
                value: false);

            migrationBuilder.UpdateData(
                table: "PacotesTuristicos",
                keyColumn: "Id",
                keyValue: 2L,
                column: "EstaDeletado",
                value: false);

            migrationBuilder.UpdateData(
                table: "PacotesTuristicos",
                keyColumn: "Id",
                keyValue: 3L,
                column: "EstaDeletado",
                value: false);

            migrationBuilder.UpdateData(
                table: "PacotesTuristicos",
                keyColumn: "Id",
                keyValue: 4L,
                column: "EstaDeletado",
                value: false);

            migrationBuilder.UpdateData(
                table: "PacotesTuristicos",
                keyColumn: "Id",
                keyValue: 5L,
                column: "EstaDeletado",
                value: false);

            migrationBuilder.UpdateData(
                table: "PacotesTuristicos",
                keyColumn: "Id",
                keyValue: 6L,
                column: "EstaDeletado",
                value: false);

            migrationBuilder.UpdateData(
                table: "PacotesTuristicos",
                keyColumn: "Id",
                keyValue: 7L,
                column: "EstaDeletado",
                value: false);

            migrationBuilder.UpdateData(
                table: "PacotesTuristicos",
                keyColumn: "Id",
                keyValue: 8L,
                column: "EstaDeletado",
                value: false);

            migrationBuilder.UpdateData(
                table: "PacotesTuristicos",
                keyColumn: "Id",
                keyValue: 9L,
                column: "EstaDeletado",
                value: false);

            migrationBuilder.UpdateData(
                table: "PacotesTuristicos",
                keyColumn: "Id",
                keyValue: 10L,
                column: "EstaDeletado",
                value: false);

            migrationBuilder.UpdateData(
                table: "PacotesTuristicos",
                keyColumn: "Id",
                keyValue: 11L,
                column: "EstaDeletado",
                value: false);

            migrationBuilder.UpdateData(
                table: "PacotesTuristicos",
                keyColumn: "Id",
                keyValue: 12L,
                column: "EstaDeletado",
                value: false);

            migrationBuilder.UpdateData(
                table: "PacotesTuristicos",
                keyColumn: "Id",
                keyValue: 13L,
                column: "EstaDeletado",
                value: false);

            migrationBuilder.UpdateData(
                table: "PacotesTuristicos",
                keyColumn: "Id",
                keyValue: 14L,
                column: "EstaDeletado",
                value: false);

            migrationBuilder.UpdateData(
                table: "PacotesTuristicos",
                keyColumn: "Id",
                keyValue: 15L,
                column: "EstaDeletado",
                value: false);

            migrationBuilder.UpdateData(
                table: "PacotesTuristicos",
                keyColumn: "Id",
                keyValue: 16L,
                column: "EstaDeletado",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstaDeletado",
                table: "PacotesTuristicos");
        }
    }
}
