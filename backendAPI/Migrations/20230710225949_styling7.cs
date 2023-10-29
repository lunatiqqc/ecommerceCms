using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class styling7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridColumns_Background_BackgroundId",
                table: "GridColumns");

            migrationBuilder.DropForeignKey(
                name: "FK_GridColumns_Margin_MarginId",
                table: "GridColumns");

            migrationBuilder.DropForeignKey(
                name: "FK_GridColumns_Padding_PaddingId",
                table: "GridColumns");

            migrationBuilder.DropForeignKey(
                name: "FK_GridRows_Background_BackgroundId",
                table: "GridRows");

            migrationBuilder.DropForeignKey(
                name: "FK_GridRows_Margin_MarginId",
                table: "GridRows");

            migrationBuilder.DropForeignKey(
                name: "FK_GridRows_Padding_PaddingId",
                table: "GridRows");

            migrationBuilder.DropIndex(
                name: "IX_GridRows_BackgroundId",
                table: "GridRows");

            migrationBuilder.DropIndex(
                name: "IX_GridRows_MarginId",
                table: "GridRows");

            migrationBuilder.DropIndex(
                name: "IX_GridRows_PaddingId",
                table: "GridRows");

            migrationBuilder.DropIndex(
                name: "IX_GridColumns_BackgroundId",
                table: "GridColumns");

            migrationBuilder.DropIndex(
                name: "IX_GridColumns_MarginId",
                table: "GridColumns");

            migrationBuilder.DropIndex(
                name: "IX_GridColumns_PaddingId",
                table: "GridColumns");

            migrationBuilder.DropColumn(
                name: "BackgroundId",
                table: "GridRows");

            migrationBuilder.DropColumn(
                name: "MarginId",
                table: "GridRows");

            migrationBuilder.DropColumn(
                name: "PaddingId",
                table: "GridRows");

            migrationBuilder.DropColumn(
                name: "BackgroundId",
                table: "GridColumns");

            migrationBuilder.DropColumn(
                name: "MarginId",
                table: "GridColumns");

            migrationBuilder.DropColumn(
                name: "PaddingId",
                table: "GridColumns");

            migrationBuilder.AddColumn<int>(
                name: "StyleId",
                table: "GridRows",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StyleId",
                table: "GridColumns",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Style",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PaddingId = table.Column<int>(type: "integer", nullable: true),
                    MarginId = table.Column<int>(type: "integer", nullable: true),
                    BackgroundId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Style", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Style_Background_BackgroundId",
                        column: x => x.BackgroundId,
                        principalTable: "Background",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Style_Margin_MarginId",
                        column: x => x.MarginId,
                        principalTable: "Margin",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Style_Padding_PaddingId",
                        column: x => x.PaddingId,
                        principalTable: "Padding",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GridRows_StyleId",
                table: "GridRows",
                column: "StyleId");

            migrationBuilder.CreateIndex(
                name: "IX_GridColumns_StyleId",
                table: "GridColumns",
                column: "StyleId");

            migrationBuilder.CreateIndex(
                name: "IX_Style_BackgroundId",
                table: "Style",
                column: "BackgroundId");

            migrationBuilder.CreateIndex(
                name: "IX_Style_MarginId",
                table: "Style",
                column: "MarginId");

            migrationBuilder.CreateIndex(
                name: "IX_Style_PaddingId",
                table: "Style",
                column: "PaddingId");

            migrationBuilder.AddForeignKey(
                name: "FK_GridColumns_Style_StyleId",
                table: "GridColumns",
                column: "StyleId",
                principalTable: "Style",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GridRows_Style_StyleId",
                table: "GridRows",
                column: "StyleId",
                principalTable: "Style",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridColumns_Style_StyleId",
                table: "GridColumns");

            migrationBuilder.DropForeignKey(
                name: "FK_GridRows_Style_StyleId",
                table: "GridRows");

            migrationBuilder.DropTable(
                name: "Style");

            migrationBuilder.DropIndex(
                name: "IX_GridRows_StyleId",
                table: "GridRows");

            migrationBuilder.DropIndex(
                name: "IX_GridColumns_StyleId",
                table: "GridColumns");

            migrationBuilder.DropColumn(
                name: "StyleId",
                table: "GridRows");

            migrationBuilder.DropColumn(
                name: "StyleId",
                table: "GridColumns");

            migrationBuilder.AddColumn<int>(
                name: "BackgroundId",
                table: "GridRows",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MarginId",
                table: "GridRows",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaddingId",
                table: "GridRows",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BackgroundId",
                table: "GridColumns",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MarginId",
                table: "GridColumns",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaddingId",
                table: "GridColumns",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GridRows_BackgroundId",
                table: "GridRows",
                column: "BackgroundId");

            migrationBuilder.CreateIndex(
                name: "IX_GridRows_MarginId",
                table: "GridRows",
                column: "MarginId");

            migrationBuilder.CreateIndex(
                name: "IX_GridRows_PaddingId",
                table: "GridRows",
                column: "PaddingId");

            migrationBuilder.CreateIndex(
                name: "IX_GridColumns_BackgroundId",
                table: "GridColumns",
                column: "BackgroundId");

            migrationBuilder.CreateIndex(
                name: "IX_GridColumns_MarginId",
                table: "GridColumns",
                column: "MarginId");

            migrationBuilder.CreateIndex(
                name: "IX_GridColumns_PaddingId",
                table: "GridColumns",
                column: "PaddingId");

            migrationBuilder.AddForeignKey(
                name: "FK_GridColumns_Background_BackgroundId",
                table: "GridColumns",
                column: "BackgroundId",
                principalTable: "Background",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GridColumns_Margin_MarginId",
                table: "GridColumns",
                column: "MarginId",
                principalTable: "Margin",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GridColumns_Padding_PaddingId",
                table: "GridColumns",
                column: "PaddingId",
                principalTable: "Padding",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GridRows_Background_BackgroundId",
                table: "GridRows",
                column: "BackgroundId",
                principalTable: "Background",
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
    }
}
