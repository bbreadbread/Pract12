using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pract12.Migrations
{
    /// <inheritdoc />
    public partial class AddCourceRelationManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InterestGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserInterestGroup",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    InterestGroupId = table.Column<int>(type: "int", nullable: false),
                    JoinedAt = table.Column<DateOnly>(type: "date", nullable: false),
                    IsModerator = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInterestGroup", x => new { x.UserId, x.InterestGroupId });
                    table.ForeignKey(
                        name: "FK_UserInterestGroup_InterestGroup_InterestGroupId",
                        column: x => x.InterestGroupId,
                        principalTable: "InterestGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInterestGroup_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserInterestGroup_InterestGroupId",
                table: "UserInterestGroup",
                column: "InterestGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserInterestGroup");

            migrationBuilder.DropTable(
                name: "InterestGroup");
        }
    }
}
