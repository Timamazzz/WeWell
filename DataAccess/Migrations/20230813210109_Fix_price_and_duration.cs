using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Fix_price_and_duration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adres",
                table: "Places");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Places",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Meetings",
                newName: "MinPrice");

            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Meetings",
                newName: "MinDurationHours");

            migrationBuilder.AddColumn<int>(
                name: "MaxDurationHours",
                table: "Places",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxPrice",
                table: "Places",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinDurationHours",
                table: "Places",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinPrice",
                table: "Places",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxDurationHours",
                table: "Meetings",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxPrice",
                table: "Meetings",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxDurationHours",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "MaxPrice",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "MinDurationHours",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "MinPrice",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "MaxDurationHours",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "MaxPrice",
                table: "Meetings");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Places",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "MinPrice",
                table: "Meetings",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "MinDurationHours",
                table: "Meetings",
                newName: "Duration");

            migrationBuilder.AddColumn<string>(
                name: "Adres",
                table: "Places",
                type: "text",
                nullable: true);
        }
    }
}
