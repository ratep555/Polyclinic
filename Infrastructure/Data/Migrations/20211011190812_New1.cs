using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.migrations
{
    public partial class New1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Office1Id",
                table: "Offices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offices_Office1Id",
                table: "Offices",
                column: "Office1Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Offices_Offices_Office1Id",
                table: "Offices",
                column: "Office1Id",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offices_Offices_Office1Id",
                table: "Offices");

            migrationBuilder.DropIndex(
                name: "IX_Offices_Office1Id",
                table: "Offices");

            migrationBuilder.DropColumn(
                name: "Office1Id",
                table: "Offices");
        }
    }
}
