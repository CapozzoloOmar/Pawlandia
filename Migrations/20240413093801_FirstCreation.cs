using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nuova_cartella.Migrations
{
    /// <inheritdoc />
    public partial class FirstCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Purchase",
                columns: table => new
                {
                    PurchaseId = table.Column<string>(type: "TEXT", nullable: false),
                    prodotto = table.Column<string>(type: "TEXT", nullable: true),
                    quantita = table.Column<int>(type: "INTEGER", nullable: true),
                    prezzo = table.Column<double>(type: "REAL", nullable: false),
                    Immagine = table.Column<string>(type: "TEXT", nullable: false),
                    PrezzoTotale = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase", x => x.PurchaseId);
                });

            migrationBuilder.CreateTable(
                name: "Registrazioni",
                columns: table => new
                {
                    RegistrazioneId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false),
                    nome = table.Column<string>(type: "TEXT", nullable: true),
                    cognome = table.Column<string>(type: "TEXT", nullable: true),
                    email = table.Column<string>(type: "TEXT", nullable: true),
                    password = table.Column<string>(type: "TEXT", nullable: true),
                    sesso = table.Column<string>(type: "TEXT", nullable: true),
                    ruolo = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrazioni", x => x.RegistrazioneId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Purchase");

            migrationBuilder.DropTable(
                name: "Registrazioni");
        }
    }
}
