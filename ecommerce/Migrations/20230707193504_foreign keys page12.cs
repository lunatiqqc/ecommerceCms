using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class foreignkeyspage12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridColumns_GridContent_GridContentId",
                table: "GridColumns");

            migrationBuilder.DropForeignKey(
                name: "FK_Pages_GridContent_GridContentId",
                table: "Pages");

            migrationBuilder.AlterColumn<int>(
                name: "GridContentId",
                table: "Pages",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "GridContentId",
                table: "GridColumns",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_GridColumns_GridContent_GridContentId",
                table: "GridColumns",
                column: "GridContentId",
                principalTable: "GridContent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_GridContent_GridContentId",
                table: "Pages",
                column: "GridContentId",
                principalTable: "GridContent",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridColumns_GridContent_GridContentId",
                table: "GridColumns");

            migrationBuilder.DropForeignKey(
                name: "FK_Pages_GridContent_GridContentId",
                table: "Pages");

            migrationBuilder.AlterColumn<int>(
                name: "GridContentId",
                table: "Pages",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GridContentId",
                table: "GridColumns",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GridColumns_GridContent_GridContentId",
                table: "GridColumns",
                column: "GridContentId",
                principalTable: "GridContent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_GridContent_GridContentId",
                table: "Pages",
                column: "GridContentId",
                principalTable: "GridContent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
