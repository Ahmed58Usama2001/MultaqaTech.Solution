using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultaqaTech.Repository.Migrations
{
    /// <inheritdoc />
    public partial class ZoomMeetingCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ZoomMeetingCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoomMeetingCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZoomMeetings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AuthorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeetingId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZoomMeetingCategoryId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoomMeetings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZoomMeetings_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ZoomMeetings_ZoomMeetingCategories_ZoomMeetingCategoryId",
                        column: x => x.ZoomMeetingCategoryId,
                        principalTable: "ZoomMeetingCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ZoomMeetings_SubjectId",
                table: "ZoomMeetings",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoomMeetings_ZoomMeetingCategoryId",
                table: "ZoomMeetings",
                column: "ZoomMeetingCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZoomMeetings");

            migrationBuilder.DropTable(
                name: "ZoomMeetingCategories");
        }
    }
}
