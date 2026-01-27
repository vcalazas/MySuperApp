using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSP.Data.Migrations
{
    /// <inheritdoc />
    public partial class crudsystemssettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Register",
                table: "MSPSystemSettings",
                newName: "DTUpdate");

            migrationBuilder.AddColumn<DateTime>(
                name: "DTBegin",
                table: "MSPSystemSettings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DTEnd",
                table: "MSPSystemSettings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DTBegin",
                table: "MSPSystemSettings");

            migrationBuilder.DropColumn(
                name: "DTEnd",
                table: "MSPSystemSettings");

            migrationBuilder.RenameColumn(
                name: "DTUpdate",
                table: "MSPSystemSettings",
                newName: "Register");
        }
    }
}
