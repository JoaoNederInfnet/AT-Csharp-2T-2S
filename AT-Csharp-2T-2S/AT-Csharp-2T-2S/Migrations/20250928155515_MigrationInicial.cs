using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AT_Csharp_2T_2S.Migrations
{
    /// <inheritdoc />
    public partial class MigrationInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PacotesTuristicos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CapacidadeMaxima = table.Column<int>(type: "INTEGER", nullable: false),
                    Preco = table.Column<decimal>(type: "TEXT", nullable: false),
                    Dias = table.Column<int>(type: "INTEGER", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacotesTuristicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaisesDestino",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaisesDestino", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PrecoTotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    ClienteId = table.Column<long>(type: "INTEGER", nullable: false),
                    PacoteTuristicoId = table.Column<long>(type: "INTEGER", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservas_PacotesTuristicos_PacoteTuristicoId",
                        column: x => x.PacoteTuristicoId,
                        principalTable: "PacotesTuristicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CidadesDestino",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    PaisDestinoId = table.Column<long>(type: "INTEGER", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CidadesDestino", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CidadesDestino_PaisesDestino_PaisDestinoId",
                        column: x => x.PaisDestinoId,
                        principalTable: "PaisesDestino",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CidadeDestinoPacoteTuristico",
                columns: table => new
                {
                    DestinosId = table.Column<long>(type: "INTEGER", nullable: false),
                    PacotesTuristicosId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CidadeDestinoPacoteTuristico", x => new { x.DestinosId, x.PacotesTuristicosId });
                    table.ForeignKey(
                        name: "FK_CidadeDestinoPacoteTuristico_CidadesDestino_DestinosId",
                        column: x => x.DestinosId,
                        principalTable: "CidadesDestino",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CidadeDestinoPacoteTuristico_PacotesTuristicos_PacotesTuristicosId",
                        column: x => x.PacotesTuristicosId,
                        principalTable: "PacotesTuristicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PacotesTuristicos",
                columns: new[] { "Id", "AtualizadoEm", "CapacidadeMaxima", "CriadoEm", "DataInicio", "Dias", "Preco", "Titulo" },
                values: new object[,]
                {
                    { 1L, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), 100, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 550.00m, "Comer" },
                    { 2L, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), 150, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 750.00m, "Beber" },
                    { 3L, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), 50, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 400.00m, "Cair" },
                    { 4L, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), 30, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 600.00m, "Levantar" },
                    { 5L, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), 40, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 800.00m, "Manger" },
                    { 6L, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), 60, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 950.00m, "Boire" },
                    { 7L, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), 30, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 700.00m, "Tomber" },
                    { 8L, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), 80, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 1100.00m, "Se lever" },
                    { 9L, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), 25, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 1500.00m, "Taberu" },
                    { 10L, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), 50, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 1800.00m, "Nomu" },
                    { 11L, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), 40, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 1300.00m, "Ochiru" },
                    { 12L, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), 100, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 2000.00m, "Tatsu" },
                    { 13L, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), 70, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 1200.00m, "Chī" },
                    { 14L, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), 40, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 1000.00m, "Hē" },
                    { 15L, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), 50, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 1400.00m, "Shuāidǎo" },
                    { 16L, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), 20, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2500.00m, "Qǐlái" }
                });

            migrationBuilder.InsertData(
                table: "PaisesDestino",
                columns: new[] { "Id", "AtualizadoEm", "CriadoEm", "Nome" },
                values: new object[,]
                {
                    { 1L, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Brasil" },
                    { 2L, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), "França" },
                    { 3L, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Japão" },
                    { 4L, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), "China" }
                });

            migrationBuilder.InsertData(
                table: "CidadesDestino",
                columns: new[] { "Id", "AtualizadoEm", "CriadoEm", "Nome", "PaisDestinoId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Rio de Janeiro", 1L },
                    { 2L, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Paris", 2L },
                    { 3L, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Tóquio", 3L },
                    { 4L, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Hong Kong", 4L }
                });

            migrationBuilder.InsertData(
                table: "CidadeDestinoPacoteTuristico",
                columns: new[] { "DestinosId", "PacotesTuristicosId" },
                values: new object[,]
                {
                    { 1L, 1L },
                    { 1L, 2L },
                    { 1L, 3L },
                    { 1L, 4L },
                    { 2L, 5L },
                    { 2L, 6L },
                    { 2L, 7L },
                    { 2L, 8L },
                    { 3L, 9L },
                    { 3L, 10L },
                    { 3L, 11L },
                    { 3L, 12L },
                    { 4L, 13L },
                    { 4L, 14L },
                    { 4L, 15L },
                    { 4L, 16L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CidadeDestinoPacoteTuristico_PacotesTuristicosId",
                table: "CidadeDestinoPacoteTuristico",
                column: "PacotesTuristicosId");

            migrationBuilder.CreateIndex(
                name: "IX_CidadesDestino_PaisDestinoId",
                table: "CidadesDestino",
                column: "PaisDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_ClienteId",
                table: "Reservas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_PacoteTuristicoId",
                table: "Reservas",
                column: "PacoteTuristicoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CidadeDestinoPacoteTuristico");

            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "CidadesDestino");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "PacotesTuristicos");

            migrationBuilder.DropTable(
                name: "PaisesDestino");
        }
    }
}
