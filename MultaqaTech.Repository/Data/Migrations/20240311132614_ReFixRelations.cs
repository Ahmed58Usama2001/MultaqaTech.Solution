using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultaqaTech.Repository.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReFixRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseCoursePrerequist");

            migrationBuilder.DropTable(
                name: "CourseCourseTag");

            migrationBuilder.RenameColumn(
                name: "PrerrequistId",
                table: "CoursesPrerequists",
                newName: "PrerequistId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursesTags_CourseId",
                table: "CoursesTags",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursesPrerequists_CourseId",
                table: "CoursesPrerequists",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesPrerequists_Courses_CourseId",
                table: "CoursesPrerequists",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesPrerequists_Subjects_PrerequistId",
                table: "CoursesPrerequists",
                column: "PrerequistId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesTags_Courses_CourseId",
                table: "CoursesTags",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesTags_Subjects_TagId",
                table: "CoursesTags",
                column: "TagId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursesPrerequists_Courses_CourseId",
                table: "CoursesPrerequists");

            migrationBuilder.DropForeignKey(
                name: "FK_CoursesPrerequists_Subjects_PrerequistId",
                table: "CoursesPrerequists");

            migrationBuilder.DropForeignKey(
                name: "FK_CoursesTags_Courses_CourseId",
                table: "CoursesTags");

            migrationBuilder.DropForeignKey(
                name: "FK_CoursesTags_Subjects_TagId",
                table: "CoursesTags");

            migrationBuilder.DropIndex(
                name: "IX_CoursesTags_CourseId",
                table: "CoursesTags");

            migrationBuilder.DropIndex(
                name: "IX_CoursesPrerequists_CourseId",
                table: "CoursesPrerequists");

            migrationBuilder.RenameColumn(
                name: "PrerequistId",
                table: "CoursesPrerequists",
                newName: "PrerrequistId");

            migrationBuilder.CreateTable(
                name: "CourseCoursePrerequist",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    PrerequisitesPrerrequistId = table.Column<int>(type: "int", nullable: false),
                    PrerequisitesCourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCoursePrerequist", x => new { x.CourseId, x.PrerequisitesPrerrequistId, x.PrerequisitesCourseId });
                    table.ForeignKey(
                        name: "FK_CourseCoursePrerequist_CoursesPrerequists_PrerequisitesPrerrequistId_PrerequisitesCourseId",
                        columns: x => new { x.PrerequisitesPrerrequistId, x.PrerequisitesCourseId },
                        principalTable: "CoursesPrerequists",
                        principalColumns: new[] { "PrerrequistId", "CourseId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseCoursePrerequist_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseCourseTag",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    TagsTagId = table.Column<int>(type: "int", nullable: false),
                    TagsCourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCourseTag", x => new { x.CourseId, x.TagsTagId, x.TagsCourseId });
                    table.ForeignKey(
                        name: "FK_CourseCourseTag_CoursesTags_TagsTagId_TagsCourseId",
                        columns: x => new { x.TagsTagId, x.TagsCourseId },
                        principalTable: "CoursesTags",
                        principalColumns: new[] { "TagId", "CourseId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseCourseTag_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseCoursePrerequist_PrerequisitesPrerrequistId_PrerequisitesCourseId",
                table: "CourseCoursePrerequist",
                columns: new[] { "PrerequisitesPrerrequistId", "PrerequisitesCourseId" });

            migrationBuilder.CreateIndex(
                name: "IX_CourseCourseTag_TagsTagId_TagsCourseId",
                table: "CourseCourseTag",
                columns: new[] { "TagsTagId", "TagsCourseId" });
        }
    }
}
