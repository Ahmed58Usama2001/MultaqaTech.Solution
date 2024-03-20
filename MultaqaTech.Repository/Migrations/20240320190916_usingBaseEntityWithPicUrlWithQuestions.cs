using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultaqaTech.Repository.Migrations
{
    /// <inheritdoc />
    public partial class usingBaseEntityWithPicUrlWithQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "Questions");
        }
    }
}
