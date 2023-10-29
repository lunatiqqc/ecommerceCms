using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class styling3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GridColumns");

            migrationBuilder.DropTable(
                name: "GridRows");

            migrationBuilder.AddColumn<int>(
                name: "ColumnStart",
                table: "BaseStyling",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ComponentId",
                table: "BaseStyling",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GridColumn_GridContentId",
                table: "BaseStyling",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GridContentId",
                table: "BaseStyling",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GridRowId",
                table: "BaseStyling",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "BaseStyling",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaseStyling_ComponentId",
                table: "BaseStyling",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseStyling_GridColumn_GridContentId",
                table: "BaseStyling",
                column: "GridColumn_GridContentId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseStyling_GridContentId",
                table: "BaseStyling",
                column: "GridContentId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseStyling_GridRowId",
                table: "BaseStyling",
                column: "GridRowId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseStyling_BaseStyling_GridRowId",
                table: "BaseStyling",
                column: "GridRowId",
                principalTable: "BaseStyling",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseStyling_Component_ComponentId",
                table: "BaseStyling",
                column: "ComponentId",
                principalTable: "Component",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseStyling_GridContent_GridColumn_GridContentId",
                table: "BaseStyling",
                column: "GridColumn_GridContentId",
                principalTable: "GridContent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseStyling_GridContent_GridContentId",
                table: "BaseStyling",
                column: "GridContentId",
                principalTable: "GridContent",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseStyling_BaseStyling_GridRowId",
                table: "BaseStyling");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseStyling_Component_ComponentId",
                table: "BaseStyling");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseStyling_GridContent_GridColumn_GridContentId",
                table: "BaseStyling");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseStyling_GridContent_GridContentId",
                table: "BaseStyling");

            migrationBuilder.DropIndex(
                name: "IX_BaseStyling_ComponentId",
                table: "BaseStyling");

            migrationBuilder.DropIndex(
                name: "IX_BaseStyling_GridColumn_GridContentId",
                table: "BaseStyling");

            migrationBuilder.DropIndex(
                name: "IX_BaseStyling_GridContentId",
                table: "BaseStyling");

            migrationBuilder.DropIndex(
                name: "IX_BaseStyling_GridRowId",
                table: "BaseStyling");

            migrationBuilder.DropColumn(
                name: "ColumnStart",
                table: "BaseStyling");

            migrationBuilder.DropColumn(
                name: "ComponentId",
                table: "BaseStyling");

            migrationBuilder.DropColumn(
                name: "GridColumn_GridContentId",
                table: "BaseStyling");

            migrationBuilder.DropColumn(
                name: "GridContentId",
                table: "BaseStyling");

            migrationBuilder.DropColumn(
                name: "GridRowId",
                table: "BaseStyling");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "BaseStyling");

            migrationBuilder.CreateTable(
                name: "GridRows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GridContentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GridRows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GridRows_GridContent_GridContentId",
                        column: x => x.GridContentId,
                        principalTable: "GridContent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GridColumns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ComponentId = table.Column<int>(type: "integer", nullable: true),
                    GridContentId = table.Column<int>(type: "integer", nullable: true),
                    GridRowId = table.Column<int>(type: "integer", nullable: true),
                    ColumnStart = table.Column<int>(type: "integer", nullable: false),
                    Width = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GridColumns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GridColumns_Component_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Component",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GridColumns_GridContent_GridContentId",
                        column: x => x.GridContentId,
                        principalTable: "GridContent",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GridColumns_GridRows_GridRowId",
                        column: x => x.GridRowId,
                        principalTable: "GridRows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GridColumns_ComponentId",
                table: "GridColumns",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_GridColumns_GridContentId",
                table: "GridColumns",
                column: "GridContentId");

            migrationBuilder.CreateIndex(
                name: "IX_GridColumns_GridRowId",
                table: "GridColumns",
                column: "GridRowId");

            migrationBuilder.CreateIndex(
                name: "IX_GridRows_GridContentId",
                table: "GridRows",
                column: "GridContentId");
        }
    }
}
