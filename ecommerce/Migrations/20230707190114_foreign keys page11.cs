using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class foreignkeyspage11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridRows_Pages_PageId",
                table: "GridRows");

            migrationBuilder.DropTable(
                name: "GridColumnGridRow");

            migrationBuilder.RenameColumn(
                name: "GridRowId",
                table: "Pages",
                newName: "GridContentId");

            migrationBuilder.RenameColumn(
                name: "PageId",
                table: "GridRows",
                newName: "GridContentId");

            migrationBuilder.RenameIndex(
                name: "IX_GridRows_PageId",
                table: "GridRows",
                newName: "IX_GridRows_GridContentId");

            migrationBuilder.AddColumn<int>(
                name: "GridContentId",
                table: "GridColumns",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GridRowId",
                table: "GridColumns",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GridContent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GridContent", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pages_GridContentId",
                table: "Pages",
                column: "GridContentId");

            migrationBuilder.CreateIndex(
                name: "IX_GridColumns_GridContentId",
                table: "GridColumns",
                column: "GridContentId");

            migrationBuilder.CreateIndex(
                name: "IX_GridColumns_GridRowId",
                table: "GridColumns",
                column: "GridRowId");

            migrationBuilder.AddForeignKey(
                name: "FK_GridColumns_GridContent_GridContentId",
                table: "GridColumns",
                column: "GridContentId",
                principalTable: "GridContent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_GridContent_GridContentId",
                table: "Pages",
                column: "GridContentId",
                principalTable: "GridContent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridColumns_GridContent_GridContentId",
                table: "GridColumns");

            migrationBuilder.DropForeignKey(
                name: "FK_GridColumns_GridRows_GridRowId",
                table: "GridColumns");

            migrationBuilder.DropForeignKey(
                name: "FK_GridRows_GridContent_GridContentId",
                table: "GridRows");

            migrationBuilder.DropForeignKey(
                name: "FK_Pages_GridContent_GridContentId",
                table: "Pages");

            migrationBuilder.DropTable(
                name: "GridContent");

            migrationBuilder.DropIndex(
                name: "IX_Pages_GridContentId",
                table: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_GridColumns_GridContentId",
                table: "GridColumns");

            migrationBuilder.DropIndex(
                name: "IX_GridColumns_GridRowId",
                table: "GridColumns");

            migrationBuilder.DropColumn(
                name: "GridContentId",
                table: "GridColumns");

            migrationBuilder.DropColumn(
                name: "GridRowId",
                table: "GridColumns");

            migrationBuilder.RenameColumn(
                name: "GridContentId",
                table: "Pages",
                newName: "GridRowId");

            migrationBuilder.RenameColumn(
                name: "GridContentId",
                table: "GridRows",
                newName: "PageId");

            migrationBuilder.RenameIndex(
                name: "IX_GridRows_GridContentId",
                table: "GridRows",
                newName: "IX_GridRows_PageId");

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
                principalColumn: "Id");
        }
    }
}
