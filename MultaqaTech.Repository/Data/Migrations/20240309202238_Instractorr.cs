using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultaqaTech.Repository.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class Instractorr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Instractor_InstructorId",
                table: "Courses");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Instractor_InstructorId",
                table: "Courses",
                column: "InstructorId",
                principalTable: "Instractor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Instractor_InstructorId",
                table: "Courses");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Instractor_InstructorId",
                table: "Courses",
                column: "InstructorId",
                principalTable: "Instractor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
