using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjekatBackend.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Automobili_Ekspoziture_EkspozituraID",
                table: "Automobili");

            migrationBuilder.DropIndex(
                name: "IX_Automobili_EkspozituraID",
                table: "Automobili");

            migrationBuilder.DropColumn(
                name: "EkspozituraID",
                table: "Automobili");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EkspozituraID",
                table: "Automobili",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Automobili_EkspozituraID",
                table: "Automobili",
                column: "EkspozituraID");

            migrationBuilder.AddForeignKey(
                name: "FK_Automobili_Ekspoziture_EkspozituraID",
                table: "Automobili",
                column: "EkspozituraID",
                principalTable: "Ekspoziture",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
