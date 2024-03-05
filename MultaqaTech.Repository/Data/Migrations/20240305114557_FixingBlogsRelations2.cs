using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultaqaTech.Repository.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixingBlogsRelations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_BlogPostCategories_CategoryId",
                table: "BlogPosts");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "BlogPosts",
                newName: "BlogPostCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPosts_CategoryId",
                table: "BlogPosts",
                newName: "IX_BlogPosts_BlogPostCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_BlogPostCategories_BlogPostCategoryId",
                table: "BlogPosts",
                column: "BlogPostCategoryId",
                principalTable: "BlogPostCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_BlogPostCategories_BlogPostCategoryId",
                table: "BlogPosts");

            migrationBuilder.RenameColumn(
                name: "BlogPostCategoryId",
                table: "BlogPosts",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPosts_BlogPostCategoryId",
                table: "BlogPosts",
                newName: "IX_BlogPosts_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_BlogPostCategories_CategoryId",
                table: "BlogPosts",
                column: "CategoryId",
                principalTable: "BlogPostCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
