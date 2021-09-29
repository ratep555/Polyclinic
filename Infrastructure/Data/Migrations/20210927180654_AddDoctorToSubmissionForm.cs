using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class AddDoctorToSubmissionForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "SubmissionForms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionForms_DoctorId",
                table: "SubmissionForms",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionForms_Doctors_DoctorId",
                table: "SubmissionForms",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionForms_Doctors_DoctorId",
                table: "SubmissionForms");

            migrationBuilder.DropIndex(
                name: "IX_SubmissionForms_DoctorId",
                table: "SubmissionForms");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "SubmissionForms");
        }
    }
}
