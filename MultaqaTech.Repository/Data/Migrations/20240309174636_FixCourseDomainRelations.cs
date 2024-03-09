using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultaqaTech.Repository.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixCourseDomainRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseSubject_Courses_CoursesId",
                table: "CourseSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSubject1_Courses_CourseId",
                table: "CourseSubject1");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "CourseSubject1",
                newName: "Course1Id");

            migrationBuilder.RenameColumn(
                name: "CoursesId",
                table: "CourseSubject",
                newName: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSubject_Courses_CourseId",
                table: "CourseSubject",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSubject1_Courses_Course1Id",
                table: "CourseSubject1",
                column: "Course1Id",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseSubject_Courses_CourseId",
                table: "CourseSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSubject1_Courses_Course1Id",
                table: "CourseSubject1");

            migrationBuilder.RenameColumn(
                name: "Course1Id",
                table: "CourseSubject1",
                newName: "CourseId");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "CourseSubject",
                newName: "CoursesId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSubject_Courses_CoursesId",
                table: "CourseSubject",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSubject1_Courses_CourseId",
                table: "CourseSubject1",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
