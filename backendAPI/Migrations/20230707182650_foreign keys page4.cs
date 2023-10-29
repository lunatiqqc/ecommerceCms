using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class foreignkeyspage4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridRows_GridColumns_GridColumnId",
                table: "GridRows");

            migrationBuilder.DropIndex(
                name: "IX_GridRows_GridColumnId",
                table: "GridRows");

            migrationBuilder.DropColumn(
                name: "GridColumnId",
                table: "GridRows");

            migrationBuilder.AlterColumn<int>(
                name: "GridRowId",
                table: "GridColumns",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_GridColumns_GridRowId",
                table: "GridColumns",
                column: "GridRowId");

            migrationBuilder.AddForeignKey(
                name: "FK_GridColumns_GridRows_GridRowId",
                table: "GridColumns",
                column: "GridRowId",
                principalTable: "GridRows",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridColumns_GridRows_GridRowId",
                table: "GridColumns");

            migrationBuilder.DropIndex(
                name: "IX_GridColumns_GridRowId",
                table: "GridColumns");

            migrationBuilder.AddColumn<int>(
                name: "GridColumnId",
                table: "GridRows",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GridRowId",
                table: "GridColumns",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GridRows_GridColumnId",
                table: "GridRows",
                column: "GridColumnId");

            migrationBuilder.AddForeignKey(
                name: "FK_GridRows_GridColumns_GridColumnId",
                table: "GridRows",
                column: "GridColumnId",
                principalTable: "GridColumns",
                principalColumn: "Id");
        }
    }
}
