using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultaqaTech.Repository.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifyBlogPostEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "BlogPostComments",
                newName: "CommentContent");

            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "BlogPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "BlogPostComments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "BlogPostComments");

            migrationBuilder.RenameColumn(
                name: "CommentContent",
                table: "BlogPostComments",
                newName: "Content");
        }
    }
}
