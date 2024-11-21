using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpendSmart.Migrations
{
    /// <inheritdoc />
    public partial class RenameValuesToCodeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Codes",
                newName: "CodeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
            name: "CodeId", // New column name
            table: "Codes",
            newName: "Values"); // Old column name
        }
    }
}
