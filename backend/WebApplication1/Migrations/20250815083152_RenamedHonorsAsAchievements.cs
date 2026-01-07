using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class RenamedHonorsAsAchievements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Honors_Users_UserId",
                table: "Honors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Honors",
                table: "Honors");

            migrationBuilder.RenameTable(
                name: "Honors",
                newName: "Achievements");

            migrationBuilder.RenameIndex(
                name: "IX_Honors_UserId",
                table: "Achievements",
                newName: "IX_Achievements_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Achievements",
                table: "Achievements",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Achievements_Users_UserId",
                table: "Achievements",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achievements_Users_UserId",
                table: "Achievements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Achievements",
                table: "Achievements");

            migrationBuilder.RenameTable(
                name: "Achievements",
                newName: "Honors");

            migrationBuilder.RenameIndex(
                name: "IX_Achievements_UserId",
                table: "Honors",
                newName: "IX_Honors_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Honors",
                table: "Honors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Honors_Users_UserId",
                table: "Honors",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
