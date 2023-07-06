using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class resettest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridColumns_GridRows_GridRowId",
                table: "GridColumns");

            migrationBuilder.DropTable(
                name: "NestedGridColumnNestedGridRow");

            migrationBuilder.DropTable(
                name: "NestedGridColumns");

            migrationBuilder.DropTable(
                name: "NestedGridRows");

            migrationBuilder.RenameColumn(
                name: "GridRowId",
                table: "GridColumns",
                newName: "ComponentId");

            migrationBuilder.RenameIndex(
                name: "IX_GridColumns_GridRowId",
                table: "GridColumns",
                newName: "IX_GridColumns_ComponentId");

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
                name: "FK_GridColumns_Component_ComponentId",
                table: "GridColumns",
                column: "ComponentId",
                principalTable: "Component",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridColumns_Component_ComponentId",
                table: "GridColumns");

            migrationBuilder.DropTable(
                name: "GridColumnGridRow");

            migrationBuilder.RenameColumn(
                name: "ComponentId",
                table: "GridColumns",
                newName: "GridRowId");

            migrationBuilder.RenameIndex(
                name: "IX_GridColumns_ComponentId",
                table: "GridColumns",
                newName: "IX_GridColumns_GridRowId");

            migrationBuilder.CreateTable(
                name: "NestedGridColumns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ComponentId = table.Column<int>(type: "integer", nullable: true),
                    ColumnStart = table.Column<int>(type: "integer", nullable: false),
                    Width = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NestedGridColumns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NestedGridColumns_Component_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Component",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NestedGridRows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GridColumnId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NestedGridRows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NestedGridRows_GridColumns_GridColumnId",
                        column: x => x.GridColumnId,
                        principalTable: "GridColumns",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NestedGridColumnNestedGridRow",
                columns: table => new
                {
                    ColumnsId = table.Column<int>(type: "integer", nullable: false),
                    GridRowsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NestedGridColumnNestedGridRow", x => new { x.ColumnsId, x.GridRowsId });
                    table.ForeignKey(
                        name: "FK_NestedGridColumnNestedGridRow_NestedGridColumns_ColumnsId",
                        column: x => x.ColumnsId,
                        principalTable: "NestedGridColumns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NestedGridColumnNestedGridRow_NestedGridRows_GridRowsId",
                        column: x => x.GridRowsId,
                        principalTable: "NestedGridRows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NestedGridColumnNestedGridRow_GridRowsId",
                table: "NestedGridColumnNestedGridRow",
                column: "GridRowsId");

            migrationBuilder.CreateIndex(
                name: "IX_NestedGridColumns_ComponentId",
                table: "NestedGridColumns",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_NestedGridRows_GridColumnId",
                table: "NestedGridRows",
                column: "GridColumnId");

            migrationBuilder.AddForeignKey(
                name: "FK_GridColumns_GridRows_GridRowId",
                table: "GridColumns",
                column: "GridRowId",
                principalTable: "GridRows",
                principalColumn: "Id");
        }
    }
}
