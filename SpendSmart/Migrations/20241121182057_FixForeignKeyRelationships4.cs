using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpendSmart.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKeyRelationships4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Codes_Id",
                table: "Expenses");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Expenses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CodeId",
                table: "Expenses",
                column: "CodeId");

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

            migrationBuilder.DropIndex(
                name: "IX_Expenses_CodeId",
                table: "Expenses");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Expenses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Codes_Id",
                table: "Expenses",
                column: "Id",
                principalTable: "Codes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
