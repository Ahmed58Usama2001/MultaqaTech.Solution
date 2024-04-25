using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultaqaTech.Repository.Migrations
{
    /// <inheritdoc />
    public partial class minorchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizQuestionChoices_QuizQuestions_QuizQuestionId",
                table: "QuizQuestionChoices");

            migrationBuilder.DropForeignKey(
                name: "FK_ZoomMeetings_Subjects_SubjectId",
                table: "ZoomMeetings");

            migrationBuilder.DropIndex(
                name: "IX_ZoomMeetings_SubjectId",
                table: "ZoomMeetings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizQuestions",
                table: "QuizQuestions");

            migrationBuilder.DropIndex(
                name: "IX_QuizQuestions_QuizId",
                table: "QuizQuestions");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "ZoomMeetings");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "QuizQuestions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizQuestions",
                table: "QuizQuestions",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_WriterStudentId",
                table: "Notes",
                column: "WriterStudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestionChoices_QuizQuestions_QuizQuestionId",
                table: "QuizQuestionChoices",
                column: "QuizQuestionId",
                principalTable: "QuizQuestions",
                principalColumn: "QuizId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizQuestionChoices_QuizQuestions_QuizQuestionId",
                table: "QuizQuestionChoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizQuestions",
                table: "QuizQuestions");

            migrationBuilder.DropIndex(
                name: "IX_Notes_WriterStudentId",
                table: "Notes");

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "ZoomMeetings",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "QuizQuestions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizQuestions",
                table: "QuizQuestions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ZoomMeetings_SubjectId",
                table: "ZoomMeetings",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestions_QuizId",
                table: "QuizQuestions",
                column: "QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestionChoices_QuizQuestions_QuizQuestionId",
                table: "QuizQuestionChoices",
                column: "QuizQuestionId",
                principalTable: "QuizQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ZoomMeetings_Subjects_SubjectId",
                table: "ZoomMeetings",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");
        }
    }
}
