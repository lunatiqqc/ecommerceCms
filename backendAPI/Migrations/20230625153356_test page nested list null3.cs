using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class testpagenestedlistnull3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NestedGridColumn_Component_ComponentId",
                table: "NestedGridColumn");

            migrationBuilder.DropForeignKey(
                name: "FK_NestedGridColumnNestedGridRow_NestedGridColumn_ColumnsId",
                table: "NestedGridColumnNestedGridRow");

            migrationBuilder.DropForeignKey(
                name: "FK_NestedGridColumnNestedGridRow_NestedGridRow_GridRowsId",
                table: "NestedGridColumnNestedGridRow");

            migrationBuilder.DropForeignKey(
                name: "FK_NestedGridRow_GridColumns_GridColumnId",
                table: "NestedGridRow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NestedGridRow",
                table: "NestedGridRow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NestedGridColumn",
                table: "NestedGridColumn");

            migrationBuilder.RenameTable(
                name: "NestedGridRow",
                newName: "NestedGridRows");

            migrationBuilder.RenameTable(
                name: "NestedGridColumn",
                newName: "NestedGridColumns");

            migrationBuilder.RenameIndex(
                name: "IX_NestedGridRow_GridColumnId",
                table: "NestedGridRows",
                newName: "IX_NestedGridRows_GridColumnId");

            migrationBuilder.RenameIndex(
                name: "IX_NestedGridColumn_ComponentId",
                table: "NestedGridColumns",
                newName: "IX_NestedGridColumns_ComponentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NestedGridRows",
                table: "NestedGridRows",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NestedGridColumns",
                table: "NestedGridColumns",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NestedGridColumnNestedGridRow_NestedGridColumns_ColumnsId",
                table: "NestedGridColumnNestedGridRow",
                column: "ColumnsId",
                principalTable: "NestedGridColumns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NestedGridColumnNestedGridRow_NestedGridRows_GridRowsId",
                table: "NestedGridColumnNestedGridRow",
                column: "GridRowsId",
                principalTable: "NestedGridRows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NestedGridColumns_Component_ComponentId",
                table: "NestedGridColumns",
                column: "ComponentId",
                principalTable: "Component",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NestedGridRows_GridColumns_GridColumnId",
                table: "NestedGridRows",
                column: "GridColumnId",
                principalTable: "GridColumns",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NestedGridColumnNestedGridRow_NestedGridColumns_ColumnsId",
                table: "NestedGridColumnNestedGridRow");

            migrationBuilder.DropForeignKey(
                name: "FK_NestedGridColumnNestedGridRow_NestedGridRows_GridRowsId",
                table: "NestedGridColumnNestedGridRow");

            migrationBuilder.DropForeignKey(
                name: "FK_NestedGridColumns_Component_ComponentId",
                table: "NestedGridColumns");

            migrationBuilder.DropForeignKey(
                name: "FK_NestedGridRows_GridColumns_GridColumnId",
                table: "NestedGridRows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NestedGridRows",
                table: "NestedGridRows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NestedGridColumns",
                table: "NestedGridColumns");

            migrationBuilder.RenameTable(
                name: "NestedGridRows",
                newName: "NestedGridRow");

            migrationBuilder.RenameTable(
                name: "NestedGridColumns",
                newName: "NestedGridColumn");

            migrationBuilder.RenameIndex(
                name: "IX_NestedGridRows_GridColumnId",
                table: "NestedGridRow",
                newName: "IX_NestedGridRow_GridColumnId");

            migrationBuilder.RenameIndex(
                name: "IX_NestedGridColumns_ComponentId",
                table: "NestedGridColumn",
                newName: "IX_NestedGridColumn_ComponentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NestedGridRow",
                table: "NestedGridRow",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NestedGridColumn",
                table: "NestedGridColumn",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NestedGridColumn_Component_ComponentId",
                table: "NestedGridColumn",
                column: "ComponentId",
                principalTable: "Component",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NestedGridColumnNestedGridRow_NestedGridColumn_ColumnsId",
                table: "NestedGridColumnNestedGridRow",
                column: "ColumnsId",
                principalTable: "NestedGridColumn",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NestedGridColumnNestedGridRow_NestedGridRow_GridRowsId",
                table: "NestedGridColumnNestedGridRow",
                column: "GridRowsId",
                principalTable: "NestedGridRow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NestedGridRow_GridColumns_GridColumnId",
                table: "NestedGridRow",
                column: "GridColumnId",
                principalTable: "GridColumns",
                principalColumn: "Id");
        }
    }
}
