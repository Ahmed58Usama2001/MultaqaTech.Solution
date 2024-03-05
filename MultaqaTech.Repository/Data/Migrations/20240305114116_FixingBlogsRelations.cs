using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultaqaTech.Repository.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixingBlogsRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_BlogPostCategories_BlogPostCategoryId",
                table: "BlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostSubject_BlogPosts_BlogPostId",
                table: "BlogPostSubject");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_BlogPostCategoryId",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "NumberOfLectures",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "BlogPostCategoryId",
                table: "BlogPosts");

            migrationBuilder.RenameColumn(
                name: "BlogPostId",
                table: "BlogPostSubject",
                newName: "BlogPostsId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostSubject_BlogPosts_BlogPostsId",
                table: "BlogPostSubject",
                column: "BlogPostsId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostSubject_BlogPosts_BlogPostsId",
                table: "BlogPostSubject");

            migrationBuilder.RenameColumn(
                name: "BlogPostsId",
                table: "BlogPostSubject",
                newName: "BlogPostId");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfLectures",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostSubject_BlogPosts_BlogPostId",
                table: "BlogPostSubject",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
