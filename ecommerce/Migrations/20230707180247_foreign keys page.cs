using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class foreignkeyspage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridRows_Pages_PageId",
                table: "GridRows");

            migrationBuilder.AlterColumn<int>(
                name: "PageId",
                table: "GridRows",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GridRowId",
                table: "GridColumns",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_GridRows_Pages_PageId",
                table: "GridRows",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridRows_Pages_PageId",
                table: "GridRows");

            migrationBuilder.DropColumn(
                name: "GridRowId",
                table: "GridColumns");

            migrationBuilder.AlterColumn<int>(
                name: "PageId",
                table: "GridRows",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_GridRows_Pages_PageId",
                table: "GridRows",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id");
        }
    }
}
