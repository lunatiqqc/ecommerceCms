using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class optionalrelationshipgridRowgridColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridColumnGridRow_GridColumns_ColumnsId",
                table: "GridColumnGridRow");

            migrationBuilder.DropForeignKey(
                name: "FK_GridColumnGridRow_GridRows_GridRowsId",
                table: "GridColumnGridRow");

            migrationBuilder.RenameColumn(
                name: "GridRowsId",
                table: "GridColumnGridRow",
                newName: "GridRowId");

            migrationBuilder.RenameColumn(
                name: "ColumnsId",
                table: "GridColumnGridRow",
                newName: "GridColumnId");

            migrationBuilder.RenameIndex(
                name: "IX_GridColumnGridRow_GridRowsId",
                table: "GridColumnGridRow",
                newName: "IX_GridColumnGridRow_GridRowId");

            migrationBuilder.AddForeignKey(
                name: "FK_GridColumnGridRow_GridColumns_GridColumnId",
                table: "GridColumnGridRow",
                column: "GridColumnId",
                principalTable: "GridColumns",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GridColumnGridRow_GridRows_GridRowId",
                table: "GridColumnGridRow",
                column: "GridRowId",
                principalTable: "GridRows",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridColumnGridRow_GridColumns_GridColumnId",
                table: "GridColumnGridRow");

            migrationBuilder.DropForeignKey(
                name: "FK_GridColumnGridRow_GridRows_GridRowId",
                table: "GridColumnGridRow");

            migrationBuilder.RenameColumn(
                name: "GridRowId",
                table: "GridColumnGridRow",
                newName: "GridRowsId");

            migrationBuilder.RenameColumn(
                name: "GridColumnId",
                table: "GridColumnGridRow",
                newName: "ColumnsId");

            migrationBuilder.RenameIndex(
                name: "IX_GridColumnGridRow_GridRowId",
                table: "GridColumnGridRow",
                newName: "IX_GridColumnGridRow_GridRowsId");

            migrationBuilder.AddForeignKey(
                name: "FK_GridColumnGridRow_GridColumns_ColumnsId",
                table: "GridColumnGridRow",
                column: "ColumnsId",
                principalTable: "GridColumns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GridColumnGridRow_GridRows_GridRowsId",
                table: "GridColumnGridRow",
                column: "GridRowsId",
                principalTable: "GridRows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
