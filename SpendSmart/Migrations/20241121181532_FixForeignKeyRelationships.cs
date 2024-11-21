using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpendSmart.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKeyRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CodeId",
                table: "Codes",
                newName: "ShortCode");

            migrationBuilder.RenameIndex(
                name: "IX_Codes_CodeId",
                table: "Codes",
                newName: "IX_Codes_ShortCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShortCode",
                table: "Codes",
                newName: "CodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Codes_ShortCode",
                table: "Codes",
                newName: "IX_Codes_CodeId");
        }
    }
}
