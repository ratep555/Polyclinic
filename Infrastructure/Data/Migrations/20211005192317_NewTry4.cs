using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class NewTry4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Doctor1Id = table.Column<int>(type: "int", nullable: false),
                    InitialExaminationFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FollowUpExaminationFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offices_Doctors1_Doctor1Id",
                        column: x => x.Doctor1Id,
                        principalTable: "Doctors1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Patient1Id = table.Column<int>(type: "int", nullable: true),
                    Office1Id = table.Column<int>(type: "int", nullable: false),
                    StartDateAndTimeOfAppointment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateAndTimeOfAppointment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments1_Offices_Office1Id",
                        column: x => x.Office1Id,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments1_Patients1_Patient1Id",
                        column: x => x.Patient1Id,
                        principalTable: "Patients1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments1_Office1Id",
                table: "Appointments1",
                column: "Office1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments1_Patient1Id",
                table: "Appointments1",
                column: "Patient1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Offices_Doctor1Id",
                table: "Offices",
                column: "Doctor1Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments1");

            migrationBuilder.DropTable(
                name: "Offices");
        }
    }
}
