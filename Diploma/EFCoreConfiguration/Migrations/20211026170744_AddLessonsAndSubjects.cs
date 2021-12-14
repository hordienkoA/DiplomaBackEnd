using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreConfiguration.Migrations
{
    public partial class AddLessonsAndSubjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectUser_AspNetUsers_TeachersId",
                table: "SubjectUser");

            migrationBuilder.DropTable(
                name: "Works");

            migrationBuilder.RenameColumn(
                name: "TeachersId",
                table: "SubjectUser",
                newName: "UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectUser_TeachersId",
                table: "SubjectUser",
                newName: "IX_SubjectUser_UsersId");

            migrationBuilder.RenameColumn(
                name: "CourseNumber",
                table: "Subjects",
                newName: "Course");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ValidTill = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lessons_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_SubjectId",
                table: "Lessons",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectUser_AspNetUsers_UsersId",
                table: "SubjectUser",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectUser_AspNetUsers_UsersId",
                table: "SubjectUser");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Subjects");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "SubjectUser",
                newName: "TeachersId");

            migrationBuilder.RenameIndex(
                name: "IX_SubjectUser_UsersId",
                table: "SubjectUser",
                newName: "IX_SubjectUser_TeachersId");

            migrationBuilder.RenameColumn(
                name: "Course",
                table: "Subjects",
                newName: "CourseNumber");

            migrationBuilder.CreateTable(
                name: "Works",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubjectId = table.Column<long>(type: "bigint", nullable: true),
                    SubjectId1 = table.Column<int>(type: "int", nullable: true),
                    Template = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Test = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Works", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Works_Subjects_SubjectId1",
                        column: x => x.SubjectId1,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Works_SubjectId1",
                table: "Works",
                column: "SubjectId1");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectUser_AspNetUsers_TeachersId",
                table: "SubjectUser",
                column: "TeachersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
