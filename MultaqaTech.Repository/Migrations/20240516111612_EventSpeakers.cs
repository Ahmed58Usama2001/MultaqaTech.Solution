using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultaqaTech.Repository.Migrations
{
    /// <inheritdoc />
    public partial class EventSpeakers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "To",
                table: "Events",
                newName: "TimeTo");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Events",
                newName: "DateTo");

            migrationBuilder.RenameColumn(
                name: "From",
                table: "Events",
                newName: "TimeFrom");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Events",
                newName: "Website");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Events",
                newName: "PhoneNumber");

            migrationBuilder.AlterColumn<string>(
                name: "Price",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "AboutTheEvent",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateFrom",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "EventSpeakers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SpeakerPictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSpeakers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventSpeakers_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventSpeakers_EventId",
                table: "EventSpeakers",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventSpeakers");

            migrationBuilder.DropColumn(
                name: "AboutTheEvent",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "DateFrom",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "Website",
                table: "Events",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "TimeTo",
                table: "Events",
                newName: "To");

            migrationBuilder.RenameColumn(
                name: "TimeFrom",
                table: "Events",
                newName: "From");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Events",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "DateTo",
                table: "Events",
                newName: "StartDate");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Events",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
