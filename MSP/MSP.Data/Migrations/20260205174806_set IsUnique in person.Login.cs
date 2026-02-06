using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSP.Data.Migrations
{
    /// <inheritdoc />
    public partial class setIsUniqueinpersonLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MSPPerson_Login",
                table: "MSPPerson",
                column: "Login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MSPPerson_Login",
                table: "MSPPerson");
        }
    }
}
