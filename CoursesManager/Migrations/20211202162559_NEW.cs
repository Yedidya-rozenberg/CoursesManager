using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoursesManager.Migrations
{
    public partial class NEW : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LestName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhonNumber = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Payment = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentID);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    TeacherID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LestName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhonNumber = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Selery = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.TeacherID);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CoursID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoursName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CuorsStatus = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    TeacherID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CoursID);
                    table.ForeignKey(
                        name: "FK_Courses_Teachers_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "Teachers",
                        principalColumn: "TeacherID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentID = table.Column<int>(type: "int", nullable: true),
                    TeacherID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Teachers_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "Teachers",
                        principalColumn: "TeacherID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CoursStudent",
                columns: table => new
                {
                    CoursID = table.Column<int>(type: "int", nullable: false),
                    StudentsStudentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursStudent", x => new { x.CoursID, x.StudentsStudentID });
                    table.ForeignKey(
                        name: "FK_CoursStudent_Courses_CoursID",
                        column: x => x.CoursID,
                        principalTable: "Courses",
                        principalColumn: "CoursID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursStudent_Students_StudentsStudentID",
                        column: x => x.StudentsStudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "requests",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoursID = table.Column<int>(type: "int", nullable: false),
                    StudentID = table.Column<int>(type: "int", nullable: true),
                    TeacherID = table.Column<int>(type: "int", nullable: true),
                    RequestCode = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    RequestDetiles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requests", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK_requests_Courses_CoursID",
                        column: x => x.CoursID,
                        principalTable: "Courses",
                        principalColumn: "CoursID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_requests_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_requests_Teachers_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "Teachers",
                        principalColumn: "TeacherID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    UnitID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    coursID = table.Column<int>(type: "int", nullable: false),
                    UnitName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudyContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Questions = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.UnitID);
                    table.ForeignKey(
                        name: "FK_Units_Courses_coursID",
                        column: x => x.coursID,
                        principalTable: "Courses",
                        principalColumn: "CoursID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TeacherID",
                table: "Courses",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_CoursStudent_StudentsStudentID",
                table: "CoursStudent",
                column: "StudentsStudentID");

            migrationBuilder.CreateIndex(
                name: "IX_requests_CoursID",
                table: "requests",
                column: "CoursID");

            migrationBuilder.CreateIndex(
                name: "IX_requests_StudentID",
                table: "requests",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_requests_TeacherID",
                table: "requests",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_Units_coursID",
                table: "Units",
                column: "coursID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_StudentID",
                table: "Users",
                column: "StudentID",
                unique: true,
                filter: "[StudentID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TeacherID",
                table: "Users",
                column: "TeacherID",
                unique: true,
                filter: "[TeacherID] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursStudent");

            migrationBuilder.DropTable(
                name: "requests");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
