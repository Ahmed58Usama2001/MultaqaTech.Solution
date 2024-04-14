using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultaqaTech.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddingNotesAndQAToEntitiesWithConfigs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Quizes_QuizId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "IsRight",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "QuizId",
                table: "Questions",
                newName: "LectureId");

            migrationBuilder.RenameColumn(
                name: "PictureUrl",
                table: "Questions",
                newName: "MediaUrl");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_QuizId",
                table: "Questions",
                newName: "IX_Questions_LectureId");

            migrationBuilder.RenameColumn(
                name: "PictureUrl",
                table: "Courses",
                newName: "MediaUrl");

            migrationBuilder.RenameColumn(
                name: "PictureUrl",
                table: "BlogPosts",
                newName: "MediaUrl");

            migrationBuilder.AddColumn<string>(
                name: "MediaUrl",
                table: "Quizes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "QuizQuestionPictureUrl",
                table: "Quizes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Questions",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MediaUrl",
                table: "Lectures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Answers",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    LectureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_Lectures_LectureId",
                        column: x => x.LectureId,
                        principalTable: "Lectures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QuizQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    MediaUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizQuestions_Quizes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QuizQuestionChoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    IsRight = table.Column<bool>(type: "bit", nullable: false),
                    QuizQuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestionChoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizQuestionChoices_QuizQuestions_QuizQuestionId",
                        column: x => x.QuizQuestionId,
                        principalTable: "QuizQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_LectureId",
                table: "Notes",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestionChoices_QuizQuestionId",
                table: "QuizQuestionChoices",
                column: "QuizQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestions_QuizId",
                table: "QuizQuestions",
                column: "QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Lectures_LectureId",
                table: "Questions",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Lectures_LectureId",
                table: "Questions");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "QuizQuestionChoices");

            migrationBuilder.DropTable(
                name: "QuizQuestions");

            migrationBuilder.DropColumn(
                name: "MediaUrl",
                table: "Quizes");

            migrationBuilder.DropColumn(
                name: "QuizQuestionPictureUrl",
                table: "Quizes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "MediaUrl",
                table: "Lectures");

            migrationBuilder.RenameColumn(
                name: "MediaUrl",
                table: "Questions",
                newName: "PictureUrl");

            migrationBuilder.RenameColumn(
                name: "LectureId",
                table: "Questions",
                newName: "QuizId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_LectureId",
                table: "Questions",
                newName: "IX_Questions_QuizId");

            migrationBuilder.RenameColumn(
                name: "MediaUrl",
                table: "Courses",
                newName: "PictureUrl");

            migrationBuilder.RenameColumn(
                name: "MediaUrl",
                table: "BlogPosts",
                newName: "PictureUrl");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Questions",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Answers",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AddColumn<bool>(
                name: "IsRight",
                table: "Answers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Answers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Quizes_QuizId",
                table: "Questions",
                column: "QuizId",
                principalTable: "Quizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
