using Microsoft.EntityFrameworkCore.Migrations;

namespace CoursesManager.Migrations
{
    public partial class UPPER : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "email",
                table: "Teachers",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Students",
                newName: "Email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Teachers",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Students",
                newName: "email");
        }
    }
}
