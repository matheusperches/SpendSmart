using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpendSmart.Migrations
{
    /// <inheritdoc />
    public partial class AddCodeExpenseRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Codes_CodeId",
                table: "Expenses");

            migrationBuilder.AlterColumn<int>(
                name: "CodeId",
                table: "Expenses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Codes_CodeId",
                table: "Expenses",
                column: "CodeId",
                principalTable: "Codes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Codes_CodeId",
                table: "Expenses");

            migrationBuilder.AlterColumn<int>(
                name: "CodeId",
                table: "Expenses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Codes_CodeId",
                table: "Expenses",
                column: "CodeId",
                principalTable: "Codes",
                principalColumn: "Id");
        }
    }
}
