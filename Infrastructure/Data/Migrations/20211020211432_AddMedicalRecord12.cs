using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class AddMedicalRecord12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_Doctors1_Doctor1Id",
                table: "MedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecords_Doctor1Id",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "Doctor1Id",
                table: "MedicalRecords");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Doctor1Id",
                table: "MedicalRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_Doctor1Id",
                table: "MedicalRecords",
                column: "Doctor1Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_Doctors1_Doctor1Id",
                table: "MedicalRecords",
                column: "Doctor1Id",
                principalTable: "Doctors1",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
