using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultaqaTech.Repository.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "QuizQuestions",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "QuizQuestionChoices",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "QuizQuestionChoices",
                newName: "Clarification");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Questions",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Notes",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Answers",
                newName: "Content");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "QuizQuestions",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "QuizQuestionChoices",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Clarification",
                table: "QuizQuestionChoices",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Questions",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Notes",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Answers",
                newName: "Description");
        }
    }
}
