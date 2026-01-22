using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSP.Data.Migrations
{
    /// <inheritdoc />
    public partial class setupSystemSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Settings",
                table: "Settings");

            migrationBuilder.RenameTable(
                name: "Settings",
                newName: "MSPSystemSettings");

            migrationBuilder.RenameIndex(
                name: "IX_Settings_SettingKey",
                table: "MSPSystemSettings",
                newName: "IX_MSPSystemSettings_SettingKey");

            migrationBuilder.AddColumn<DateTime>(
                name: "Register",
                table: "MSPSystemSettings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_MSPSystemSettings",
                table: "MSPSystemSettings",
                column: "SettingKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MSPSystemSettings",
                table: "MSPSystemSettings");

            migrationBuilder.DropColumn(
                name: "Register",
                table: "MSPSystemSettings");

            migrationBuilder.RenameTable(
                name: "MSPSystemSettings",
                newName: "Settings");

            migrationBuilder.RenameIndex(
                name: "IX_MSPSystemSettings_SettingKey",
                table: "Settings",
                newName: "IX_Settings_SettingKey");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Settings",
                table: "Settings",
                column: "SettingKey");
        }
    }
}
