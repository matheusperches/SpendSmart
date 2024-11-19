using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpendSmart.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintToCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "Codes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Codes_Value",
                table: "Codes",
                column: "Value",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Codes_Value",
                table: "Codes");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "Codes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
