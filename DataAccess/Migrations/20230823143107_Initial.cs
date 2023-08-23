using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    ImagePath = table.Column<string>(type: "text", nullable: true),
                    MinPrice = table.Column<int>(type: "integer", nullable: false),
                    MaxPrice = table.Column<int>(type: "integer", nullable: false),
                    MinDurationHours = table.Column<int>(type: "integer", nullable: false),
                    MaxDurationHours = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Preferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    AvatarPath = table.Column<string>(type: "text", nullable: true),
                    IsAllPreferences = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeetingTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PlaceId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingTypes_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlacePreference",
                columns: table => new
                {
                    PlacesId = table.Column<int>(type: "integer", nullable: false),
                    PreferencesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlacePreference", x => new { x.PlacesId, x.PreferencesId });
                    table.ForeignKey(
                        name: "FK_PlacePreference_Places_PlacesId",
                        column: x => x.PlacesId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlacePreference_Preferences_PreferencesId",
                        column: x => x.PreferencesId,
                        principalTable: "Preferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreferenceUser",
                columns: table => new
                {
                    PreferencesId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferenceUser", x => new { x.PreferencesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_PreferenceUser_Preferences_PreferencesId",
                        column: x => x.PreferencesId,
                        principalTable: "Preferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreferenceUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatorId = table.Column<int>(type: "integer", nullable: false),
                    GuestId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateTimeEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MinPrice = table.Column<int>(type: "integer", nullable: false),
                    MaxPrice = table.Column<int>(type: "integer", nullable: false),
                    MinDurationHours = table.Column<int>(type: "integer", nullable: false),
                    MaxDurationHours = table.Column<int>(type: "integer", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    PlaceId = table.Column<int>(type: "integer", nullable: false),
                    IsArchive = table.Column<bool>(type: "boolean", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    IsShowForCreator = table.Column<bool>(type: "boolean", nullable: true),
                    IsShowForGuest = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meetings_MeetingTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "MeetingTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Meetings_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Meetings_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Meetings_Users_GuestId",
                        column: x => x.GuestId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_CreatorId",
                table: "Meetings",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_GuestId",
                table: "Meetings",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_PlaceId",
                table: "Meetings",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_TypeId",
                table: "Meetings",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingTypes_PlaceId",
                table: "MeetingTypes",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_PlacePreference_PreferencesId",
                table: "PlacePreference",
                column: "PreferencesId");

            migrationBuilder.CreateIndex(
                name: "IX_PreferenceUser_UsersId",
                table: "PreferenceUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meetings");

            migrationBuilder.DropTable(
                name: "PlacePreference");

            migrationBuilder.DropTable(
                name: "PreferenceUser");

            migrationBuilder.DropTable(
                name: "MeetingTypes");

            migrationBuilder.DropTable(
                name: "Preferences");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Places");
        }
    }
}
