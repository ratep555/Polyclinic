using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class AddMedicalRecord777 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Office1Id",
                table: "MedicalRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_Office1Id",
                table: "MedicalRecords",
                column: "Office1Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_Offices_Office1Id",
                table: "MedicalRecords",
                column: "Office1Id",
                principalTable: "Offices",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_Offices_Office1Id",
                table: "MedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecords_Office1Id",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "Office1Id",
                table: "MedicalRecords");
        }
    }
}
