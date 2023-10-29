using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class cascadedeletegridcolumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridColumns_GridRows_GridRowId",
                table: "GridColumns");

            migrationBuilder.DropForeignKey(
                name: "FK_GridRows_GridContent_GridContentId",
                table: "GridRows");

            migrationBuilder.AlterColumn<int>(
                name: "GridContentId",
                table: "GridRows",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "GridRowId",
                table: "GridColumns",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GridColumns_GridRows_GridRowId",
                table: "GridColumns",
                column: "GridRowId",
                principalTable: "GridRows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GridRows_GridContent_GridContentId",
                table: "GridRows",
                column: "GridContentId",
                principalTable: "GridContent",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridColumns_GridRows_GridRowId",
                table: "GridColumns");

            migrationBuilder.DropForeignKey(
                name: "FK_GridRows_GridContent_GridContentId",
                table: "GridRows");

            migrationBuilder.AlterColumn<int>(
                name: "GridContentId",
                table: "GridRows",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GridRowId",
                table: "GridColumns",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_GridColumns_GridRows_GridRowId",
                table: "GridColumns",
                column: "GridRowId",
                principalTable: "GridRows",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GridRows_GridContent_GridContentId",
                table: "GridRows",
                column: "GridContentId",
                principalTable: "GridContent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
