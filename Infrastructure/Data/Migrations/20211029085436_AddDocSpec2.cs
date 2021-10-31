using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class AddDocSpec2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoctorSpecializations2",
                columns: table => new
                {
                    Doctor1Id = table.Column<int>(type: "int", nullable: false),
                    Specialization1Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSpecializations2", x => new { x.Doctor1Id, x.Specialization1Id });
                    table.ForeignKey(
                        name: "FK_DoctorSpecializations2_Doctors1_Doctor1Id",
                        column: x => x.Doctor1Id,
                        principalTable: "Doctors1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorSpecializations2_Specializations_Specialization1Id",
                        column: x => x.Specialization1Id,
                        principalTable: "Specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSpecializations2_Specialization1Id",
                table: "DoctorSpecializations2",
                column: "Specialization1Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorSpecializations2");
        }
    }
}
