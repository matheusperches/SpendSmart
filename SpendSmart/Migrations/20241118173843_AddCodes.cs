using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpendSmart.Migrations
{
    /// <inheritdoc />
    public partial class AddCodes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CodeId",
                table: "Expenses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Codes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Codes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CodeId",
                table: "Expenses",
                column: "CodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Codes_CodeId",
                table: "Expenses",
                column: "CodeId",
                principalTable: "Codes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Codes_CodeId",
                table: "Expenses");

            migrationBuilder.DropTable(
                name: "Codes");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_CodeId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "CodeId",
                table: "Expenses");
        }
    }
}
