using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class foreignkeyspage7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridColumns_GridRows_GridRowId",
                table: "GridColumns");

            migrationBuilder.DropIndex(
                name: "IX_GridColumns_GridRowId",
                table: "GridColumns");

            migrationBuilder.DropColumn(
                name: "GridRowId",
                table: "GridColumns");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GridColumnGridRow");

            migrationBuilder.AddColumn<int>(
                name: "GridRowId",
                table: "GridColumns",
                type: "integer",
                nullable: true);

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
    }
}
