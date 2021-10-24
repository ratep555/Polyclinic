using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class AddIcollective12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Office1Id1",
                table: "MedicalRecords",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_Office1Id1",
                table: "MedicalRecords",
                column: "Office1Id1");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_Offices_Office1Id1",
                table: "MedicalRecords",
                column: "Office1Id1",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_Offices_Office1Id1",
                table: "MedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecords_Office1Id1",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "Office1Id1",
                table: "MedicalRecords");
        }
    }
}
