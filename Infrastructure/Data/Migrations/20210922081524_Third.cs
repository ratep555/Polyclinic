using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "SubmissionForms");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "SubmissionForms");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Doctors");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "SubmissionForms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionForms_ApplicationUserId",
                table: "SubmissionForms",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_ApplicationUserId",
                table: "Patients",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_ApplicationUserId",
                table: "Doctors",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_AspNetUsers_ApplicationUserId",
                table: "Doctors",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_AspNetUsers_ApplicationUserId",
                table: "Patients",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionForms_AspNetUsers_ApplicationUserId",
                table: "SubmissionForms",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_AspNetUsers_ApplicationUserId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_AspNetUsers_ApplicationUserId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionForms_AspNetUsers_ApplicationUserId",
                table: "SubmissionForms");

            migrationBuilder.DropIndex(
                name: "IX_SubmissionForms_ApplicationUserId",
                table: "SubmissionForms");

            migrationBuilder.DropIndex(
                name: "IX_Patients_ApplicationUserId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_ApplicationUserId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "SubmissionForms");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Doctors");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "SubmissionForms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "SubmissionForms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
