using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class AddHospitalAffiliation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HospitalAffiliationId",
                table: "Offices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HospitalAffiliations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Doctor1Id = table.Column<int>(type: "int", nullable: false),
                    HospitalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalAffiliations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HospitalAffiliations_Doctors1_Doctor1Id",
                        column: x => x.Doctor1Id,
                        principalTable: "Doctors1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offices_HospitalAffiliationId",
                table: "Offices",
                column: "HospitalAffiliationId");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalAffiliations_Doctor1Id",
                table: "HospitalAffiliations",
                column: "Doctor1Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Offices_HospitalAffiliations_HospitalAffiliationId",
                table: "Offices",
                column: "HospitalAffiliationId",
                principalTable: "HospitalAffiliations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offices_HospitalAffiliations_HospitalAffiliationId",
                table: "Offices");

            migrationBuilder.DropTable(
                name: "HospitalAffiliations");

            migrationBuilder.DropIndex(
                name: "IX_Offices_HospitalAffiliationId",
                table: "Offices");

            migrationBuilder.DropColumn(
                name: "HospitalAffiliationId",
                table: "Offices");
        }
    }
}
