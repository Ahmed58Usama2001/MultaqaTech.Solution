using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultaqaTech.Repository.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostSubject_BlogPosts_BlogPostsId",
                table: "BlogPostSubject");

            migrationBuilder.RenameColumn(
                name: "BlogPostsId",
                table: "BlogPostSubject",
                newName: "BlogPostId");

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Duration = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalEnrolled = table.Column<int>(type: "int", nullable: false),
                    NumberOfLectures = table.Column<int>(type: "int", nullable: false),
                    DeductionAmount = table.Column<int>(type: "int", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CourseLevel = table.Column<int>(type: "int", nullable: false),
                    DeductionType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SubjectId",
                table: "Courses",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Title",
                table: "Courses",
                column: "Title",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostSubject_BlogPosts_BlogPostId",
                table: "BlogPostSubject",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostSubject_BlogPosts_BlogPostId",
                table: "BlogPostSubject");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.RenameColumn(
                name: "BlogPostId",
                table: "BlogPostSubject",
                newName: "BlogPostsId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostSubject_BlogPosts_BlogPostsId",
                table: "BlogPostSubject",
                column: "BlogPostsId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
