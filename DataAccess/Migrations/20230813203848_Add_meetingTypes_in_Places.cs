using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Add_meetingTypes_in_Places : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlaceId",
                table: "MeetingTypes",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeetingTypes_PlaceId",
                table: "MeetingTypes",
                column: "PlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingTypes_Places_PlaceId",
                table: "MeetingTypes",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingTypes_Places_PlaceId",
                table: "MeetingTypes");

            migrationBuilder.DropIndex(
                name: "IX_MeetingTypes_PlaceId",
                table: "MeetingTypes");

            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "MeetingTypes");
        }
    }
}
