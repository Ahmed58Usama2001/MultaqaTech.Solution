using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultaqaTech.Repository.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixCategoryProblem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlogPostCategoryId",
                table: "BlogPosts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_BlogPostCategoryId",
                table: "BlogPosts",
                column: "BlogPostCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_BlogPostCategories_BlogPostCategoryId",
                table: "BlogPosts",
                column: "BlogPostCategoryId",
                principalTable: "BlogPostCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_BlogPostCategories_BlogPostCategoryId",
                table: "BlogPosts");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_BlogPostCategoryId",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "BlogPostCategoryId",
                table: "BlogPosts");
        }
    }
}
