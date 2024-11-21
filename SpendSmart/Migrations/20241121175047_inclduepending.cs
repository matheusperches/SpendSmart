using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpendSmart.Migrations
{
    /// <inheritdoc />
    public partial class inclduepending : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Codes",
                newName: "CodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Codes_Value",
                table: "Codes",
                newName: "IX_Codes_CodeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CodeId",
                table: "Codes",
                newName: "Value");

            migrationBuilder.RenameIndex(
                name: "IX_Codes_CodeId",
                table: "Codes",
                newName: "IX_Codes_Value");
        }
    }
}
