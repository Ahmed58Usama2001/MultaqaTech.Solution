using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultaqaTech.Repository.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseSubject");

            migrationBuilder.DropTable(
                name: "CourseSubject1");

            migrationBuilder.CreateTable(
                name: "CoursesPrerequists",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    PrerrequistId = table.Column<int>(type: "int", nullable: false),
                    PrerequistName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesPrerequists", x => new { x.PrerrequistId, x.CourseId });
                });

            migrationBuilder.CreateTable(
                name: "CoursesTags",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false),
                    TagName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesTags", x => new { x.TagId, x.CourseId });
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseCoursePrerequist");

            migrationBuilder.DropTable(
                name: "CourseCourseTag");

            migrationBuilder.DropTable(
                name: "CoursesPrerequists");

            migrationBuilder.DropTable(
                name: "CoursesTags");

            migrationBuilder.CreateTable(
                name: "CourseSubject",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSubject", x => new { x.CourseId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_CourseSubject_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseSubject_Subjects_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseSubject1",
                columns: table => new
                {
                    Course1Id = table.Column<int>(type: "int", nullable: false),
                    PrerequisitesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSubject1", x => new { x.Course1Id, x.PrerequisitesId });
                    table.ForeignKey(
                        name: "FK_CourseSubject1_Courses_Course1Id",
                        column: x => x.Course1Id,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseSubject1_Subjects_PrerequisitesId",
                        column: x => x.PrerequisitesId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseSubject_TagsId",
                table: "CourseSubject",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSubject1_PrerequisitesId",
                table: "CourseSubject1",
                column: "PrerequisitesId");
        }
    }
}
