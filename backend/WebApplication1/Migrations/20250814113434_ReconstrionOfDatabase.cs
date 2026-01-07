using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class ReconstrionOfDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Experiences_UserId",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "Proficiency",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Educations");

            migrationBuilder.RenameColumn(
                name: "PhotoUrl",
                table: "Profiles",
                newName: "ProfilePic");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Profiles",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "Headline",
                table: "Profiles",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "Connections",
                table: "Profiles",
                newName: "ConnectionCount");

            migrationBuilder.RenameColumn(
                name: "About",
                table: "Profiles",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "LogoUrl",
                table: "Experiences",
                newName: "LogoPicUrl");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Profiles",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "Experiences",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Experiences",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Experiences",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsCurrent",
                table: "Experiences",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Educations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Educations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_UserId",
                table: "Experiences",
                column: "UserId",
                unique: true,
                filter: "IsCurrent = 1");

            migrationBuilder.AddCheckConstraint(
                name: "experience_date_check",
                table: "Experiences",
                sql: "StartDate < EndDate OR EndDate IS NULL");

            migrationBuilder.AddCheckConstraint(
                name: "education_date_check",
                table: "Educations",
                sql: "StartYear < EndYear");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Experiences_UserId",
                table: "Experiences");

            migrationBuilder.DropCheckConstraint(
                name: "experience_date_check",
                table: "Experiences");

            migrationBuilder.DropCheckConstraint(
                name: "education_date_check",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "IsCurrent",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Educations");

            migrationBuilder.RenameColumn(
                name: "ProfilePic",
                table: "Profiles",
                newName: "PhotoUrl");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Profiles",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Profiles",
                newName: "About");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Profiles",
                newName: "Headline");

            migrationBuilder.RenameColumn(
                name: "ConnectionCount",
                table: "Profiles",
                newName: "Connections");

            migrationBuilder.RenameColumn(
                name: "LogoPicUrl",
                table: "Experiences",
                newName: "LogoUrl");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Skills",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Proficiency",
                table: "Languages",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "Experiences",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Experiences",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Educations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_UserId",
                table: "Experiences",
                column: "UserId");
        }
    }
}
