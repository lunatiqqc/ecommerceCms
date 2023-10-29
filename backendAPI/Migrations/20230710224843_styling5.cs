using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class styling5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseStyling_Background_BackgroundId",
                table: "BaseStyling");

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

            migrationBuilder.DropForeignKey(
                name: "FK_BaseStyling_Margin_MarginId",
                table: "BaseStyling");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseStyling_Padding_PaddingId",
                table: "BaseStyling");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseStyling",
                table: "BaseStyling");

            migrationBuilder.DropIndex(
                name: "IX_BaseStyling_ComponentId",
                table: "BaseStyling");

            migrationBuilder.DropIndex(
                name: "IX_BaseStyling_GridColumn_GridContentId",
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
                name: "ContentContainer",
                table: "BaseStyling");

            migrationBuilder.DropColumn(
                name: "GridColumn_GridContentId",
                table: "BaseStyling");

            migrationBuilder.DropColumn(
                name: "GridRowId",
                table: "BaseStyling");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "BaseStyling");

            migrationBuilder.RenameTable(
                name: "BaseStyling",
                newName: "GridRows");

            migrationBuilder.RenameIndex(
                name: "IX_BaseStyling_PaddingId",
                table: "GridRows",
                newName: "IX_GridRows_PaddingId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseStyling_MarginId",
                table: "GridRows",
                newName: "IX_GridRows_MarginId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseStyling_GridContentId",
                table: "GridRows",
                newName: "IX_GridRows_GridContentId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseStyling_BackgroundId",
                table: "GridRows",
                newName: "IX_GridRows_BackgroundId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GridRows",
                table: "GridRows",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GridColumns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ColumnStart = table.Column<int>(type: "integer", nullable: false),
                    Width = table.Column<int>(type: "integer", nullable: false),
                    ComponentId = table.Column<int>(type: "integer", nullable: true),
                    GridContentId = table.Column<int>(type: "integer", nullable: true),
                    GridRowId = table.Column<int>(type: "integer", nullable: true),
                    PaddingId = table.Column<int>(type: "integer", nullable: true),
                    MarginId = table.Column<int>(type: "integer", nullable: true),
                    BackgroundId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GridColumns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GridColumns_Background_BackgroundId",
                        column: x => x.BackgroundId,
                        principalTable: "Background",
                        principalColumn: "Id");
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
                    table.ForeignKey(
                        name: "FK_GridColumns_Margin_MarginId",
                        column: x => x.MarginId,
                        principalTable: "Margin",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GridColumns_Padding_PaddingId",
                        column: x => x.PaddingId,
                        principalTable: "Padding",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GridColumns_BackgroundId",
                table: "GridColumns",
                column: "BackgroundId");

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
                name: "IX_GridColumns_MarginId",
                table: "GridColumns",
                column: "MarginId");

            migrationBuilder.CreateIndex(
                name: "IX_GridColumns_PaddingId",
                table: "GridColumns",
                column: "PaddingId");

            migrationBuilder.AddForeignKey(
                name: "FK_GridRows_Background_BackgroundId",
                table: "GridRows",
                column: "BackgroundId",
                principalTable: "Background",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GridRows_GridContent_GridContentId",
                table: "GridRows",
                column: "GridContentId",
                principalTable: "GridContent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GridRows_Margin_MarginId",
                table: "GridRows",
                column: "MarginId",
                principalTable: "Margin",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GridRows_Padding_PaddingId",
                table: "GridRows",
                column: "PaddingId",
                principalTable: "Padding",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridRows_Background_BackgroundId",
                table: "GridRows");

            migrationBuilder.DropForeignKey(
                name: "FK_GridRows_GridContent_GridContentId",
                table: "GridRows");

            migrationBuilder.DropForeignKey(
                name: "FK_GridRows_Margin_MarginId",
                table: "GridRows");

            migrationBuilder.DropForeignKey(
                name: "FK_GridRows_Padding_PaddingId",
                table: "GridRows");

            migrationBuilder.DropTable(
                name: "GridColumns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GridRows",
                table: "GridRows");

            migrationBuilder.RenameTable(
                name: "GridRows",
                newName: "BaseStyling");

            migrationBuilder.RenameIndex(
                name: "IX_GridRows_PaddingId",
                table: "BaseStyling",
                newName: "IX_BaseStyling_PaddingId");

            migrationBuilder.RenameIndex(
                name: "IX_GridRows_MarginId",
                table: "BaseStyling",
                newName: "IX_BaseStyling_MarginId");

            migrationBuilder.RenameIndex(
                name: "IX_GridRows_GridContentId",
                table: "BaseStyling",
                newName: "IX_BaseStyling_GridContentId");

            migrationBuilder.RenameIndex(
                name: "IX_GridRows_BackgroundId",
                table: "BaseStyling",
                newName: "IX_BaseStyling_BackgroundId");

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

            migrationBuilder.AddColumn<string>(
                name: "ContentContainer",
                table: "BaseStyling",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "GridColumn_GridContentId",
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

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseStyling",
                table: "BaseStyling",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BaseStyling_ComponentId",
                table: "BaseStyling",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseStyling_GridColumn_GridContentId",
                table: "BaseStyling",
                column: "GridColumn_GridContentId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseStyling_GridRowId",
                table: "BaseStyling",
                column: "GridRowId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseStyling_Background_BackgroundId",
                table: "BaseStyling",
                column: "BackgroundId",
                principalTable: "Background",
                principalColumn: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_BaseStyling_Margin_MarginId",
                table: "BaseStyling",
                column: "MarginId",
                principalTable: "Margin",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseStyling_Padding_PaddingId",
                table: "BaseStyling",
                column: "PaddingId",
                principalTable: "Padding",
                principalColumn: "Id");
        }
    }
}
