using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class foreignkeyspage3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridRows_Pages_PageId",
                table: "GridRows");

            migrationBuilder.DropTable(
                name: "GridColumnGridRow");

            migrationBuilder.AddColumn<int>(
                name: "GridRowId",
                table: "Pages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PageId",
                table: "GridRows",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "GridColumnId",
                table: "GridRows",
                type: "integer",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_GridRows_Pages_PageId",
                table: "GridRows",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridRows_GridColumns_GridColumnId",
                table: "GridRows");

            migrationBuilder.DropForeignKey(
                name: "FK_GridRows_Pages_PageId",
                table: "GridRows");

            migrationBuilder.DropIndex(
                name: "IX_GridRows_GridColumnId",
                table: "GridRows");

            migrationBuilder.DropColumn(
                name: "GridRowId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "GridColumnId",
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

            migrationBuilder.CreateTable(
                name: "GridColumnGridRow",
                columns: table => new
                {
                    ColumnsId = table.Column<int>(type: "integer", nullable: false),
                    GridRowsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GridColumnGridRow", x => new { x.ColumnsId, x.GridRowsId });
                    table.ForeignKey(
                        name: "FK_GridColumnGridRow_GridColumns_ColumnsId",
                        column: x => x.ColumnsId,
                        principalTable: "GridColumns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GridColumnGridRow_GridRows_GridRowsId",
                        column: x => x.GridRowsId,
                        principalTable: "GridRows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GridColumnGridRow_GridRowsId",
                table: "GridColumnGridRow",
                column: "GridRowsId");

            migrationBuilder.AddForeignKey(
                name: "FK_GridRows_Pages_PageId",
                table: "GridRows",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
