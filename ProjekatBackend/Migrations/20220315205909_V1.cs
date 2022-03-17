using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjekatBackend.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ekspoziture",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Grad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ekspoziture", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Klijenti",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BrojLicneKarte = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klijenti", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Automobili",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistarskiBroj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutomobilDostupan = table.Column<bool>(type: "bit", nullable: false),
                    MarkaAuta = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GodinaProizvodnje = table.Column<int>(type: "int", nullable: false),
                    Kilometraza = table.Column<int>(type: "int", nullable: false),
                    CenaIznajmljivanja = table.Column<int>(type: "int", nullable: false),
                    EkspozituraID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Automobili", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Automobili_Ekspoziture_EkspozituraID",
                        column: x => x.EkspozituraID,
                        principalTable: "Ekspoziture",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AutoEkspozitureKlijenti",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumIznajmljivanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BrojDana = table.Column<int>(type: "int", nullable: false),
                    AutoID = table.Column<int>(type: "int", nullable: true),
                    KlijentID = table.Column<int>(type: "int", nullable: true),
                    EkspozituraID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoEkspozitureKlijenti", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AutoEkspozitureKlijenti_Automobili_AutoID",
                        column: x => x.AutoID,
                        principalTable: "Automobili",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AutoEkspozitureKlijenti_Ekspoziture_EkspozituraID",
                        column: x => x.EkspozituraID,
                        principalTable: "Ekspoziture",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AutoEkspozitureKlijenti_Klijenti_KlijentID",
                        column: x => x.KlijentID,
                        principalTable: "Klijenti",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutoEkspozitureKlijenti_AutoID",
                table: "AutoEkspozitureKlijenti",
                column: "AutoID");

            migrationBuilder.CreateIndex(
                name: "IX_AutoEkspozitureKlijenti_EkspozituraID",
                table: "AutoEkspozitureKlijenti",
                column: "EkspozituraID");

            migrationBuilder.CreateIndex(
                name: "IX_AutoEkspozitureKlijenti_KlijentID",
                table: "AutoEkspozitureKlijenti",
                column: "KlijentID");

            migrationBuilder.CreateIndex(
                name: "IX_Automobili_EkspozituraID",
                table: "Automobili",
                column: "EkspozituraID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutoEkspozitureKlijenti");

            migrationBuilder.DropTable(
                name: "Automobili");

            migrationBuilder.DropTable(
                name: "Klijenti");

            migrationBuilder.DropTable(
                name: "Ekspoziture");
        }
    }
}
