using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultaqaTech.Repository.Migrations
{
    /// <inheritdoc />
    public partial class ZoomMeeting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PictureUrl",
                table: "ZoomMeetings",
                newName: "ZoomPictureUrl");

            migrationBuilder.AlterColumn<string>(
                name: "MeetingId",
                table: "ZoomMeetings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "MediaUrl",
                table: "ZoomMeetings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MeetingUrl",
                table: "ZoomMeetings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TimeZone",
                table: "ZoomMeetings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaUrl",
                table: "ZoomMeetings");

            migrationBuilder.DropColumn(
                name: "MeetingUrl",
                table: "ZoomMeetings");

            migrationBuilder.DropColumn(
                name: "TimeZone",
                table: "ZoomMeetings");

            migrationBuilder.RenameColumn(
                name: "ZoomPictureUrl",
                table: "ZoomMeetings",
                newName: "PictureUrl");

            migrationBuilder.AlterColumn<int>(
                name: "MeetingId",
                table: "ZoomMeetings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
