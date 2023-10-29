using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class testpagenestedlistnull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GridColumnGridRow");

            migrationBuilder.AddColumn<int>(
                name: "GridRowId",
                table: "GridColumns",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NestedGridColumn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ColumnStart = table.Column<int>(type: "integer", nullable: false),
                    Width = table.Column<int>(type: "integer", nullable: false),
                    ComponentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NestedGridColumn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NestedGridColumn_Component_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Component",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NestedGridRow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GridColumnId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NestedGridRow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NestedGridRow_GridColumns_GridColumnId",
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
                        name: "FK_NestedGridColumnNestedGridRow_NestedGridColumn_ColumnsId",
                        column: x => x.ColumnsId,
                        principalTable: "NestedGridColumn",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NestedGridColumnNestedGridRow_NestedGridRow_GridRowsId",
                        column: x => x.GridRowsId,
                        principalTable: "NestedGridRow",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GridColumns_GridRowId",
                table: "GridColumns",
                column: "GridRowId");

            migrationBuilder.CreateIndex(
                name: "IX_NestedGridColumn_ComponentId",
                table: "NestedGridColumn",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_NestedGridColumnNestedGridRow_GridRowsId",
                table: "NestedGridColumnNestedGridRow",
                column: "GridRowsId");

            migrationBuilder.CreateIndex(
                name: "IX_NestedGridRow_GridColumnId",
                table: "NestedGridRow",
                column: "GridColumnId");

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

            migrationBuilder.DropTable(
                name: "NestedGridColumnNestedGridRow");

            migrationBuilder.DropTable(
                name: "NestedGridColumn");

            migrationBuilder.DropTable(
                name: "NestedGridRow");

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
    }
}
