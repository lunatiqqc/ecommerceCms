using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class styling_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContainerStyling_Style_StyleId",
                table: "ContainerStyling");

            migrationBuilder.DropForeignKey(
                name: "FK_GridColumns_ContainerStyling_ContainerStylingId",
                table: "GridColumns");

            migrationBuilder.DropForeignKey(
                name: "FK_GridRows_Style_StyleId",
                table: "GridRows");

            migrationBuilder.DropTable(
                name: "Style");

            migrationBuilder.DropIndex(
                name: "IX_ContainerStyling_StyleId",
                table: "ContainerStyling");

            migrationBuilder.DropColumn(
                name: "StyleId",
                table: "ContainerStyling");

            migrationBuilder.RenameColumn(
                name: "StyleId",
                table: "GridRows",
                newName: "StylingId");

            migrationBuilder.RenameIndex(
                name: "IX_GridRows_StyleId",
                table: "GridRows",
                newName: "IX_GridRows_StylingId");

            migrationBuilder.RenameColumn(
                name: "ContainerStylingId",
                table: "GridColumns",
                newName: "StylingId");

            migrationBuilder.RenameIndex(
                name: "IX_GridColumns_ContainerStylingId",
                table: "GridColumns",
                newName: "IX_GridColumns_StylingId");

            migrationBuilder.AddColumn<int>(
                name: "BackgroundId",
                table: "ContainerStyling",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MarginId",
                table: "ContainerStyling",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaddingId",
                table: "ContainerStyling",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContainerStyling_BackgroundId",
                table: "ContainerStyling",
                column: "BackgroundId");

            migrationBuilder.CreateIndex(
                name: "IX_ContainerStyling_MarginId",
                table: "ContainerStyling",
                column: "MarginId");

            migrationBuilder.CreateIndex(
                name: "IX_ContainerStyling_PaddingId",
                table: "ContainerStyling",
                column: "PaddingId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContainerStyling_Background_BackgroundId",
                table: "ContainerStyling",
                column: "BackgroundId",
                principalTable: "Background",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContainerStyling_Margin_MarginId",
                table: "ContainerStyling",
                column: "MarginId",
                principalTable: "Margin",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContainerStyling_Padding_PaddingId",
                table: "ContainerStyling",
                column: "PaddingId",
                principalTable: "Padding",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GridColumns_ContainerStyling_StylingId",
                table: "GridColumns",
                column: "StylingId",
                principalTable: "ContainerStyling",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GridRows_ContainerStyling_StylingId",
                table: "GridRows",
                column: "StylingId",
                principalTable: "ContainerStyling",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContainerStyling_Background_BackgroundId",
                table: "ContainerStyling");

            migrationBuilder.DropForeignKey(
                name: "FK_ContainerStyling_Margin_MarginId",
                table: "ContainerStyling");

            migrationBuilder.DropForeignKey(
                name: "FK_ContainerStyling_Padding_PaddingId",
                table: "ContainerStyling");

            migrationBuilder.DropForeignKey(
                name: "FK_GridColumns_ContainerStyling_StylingId",
                table: "GridColumns");

            migrationBuilder.DropForeignKey(
                name: "FK_GridRows_ContainerStyling_StylingId",
                table: "GridRows");

            migrationBuilder.DropIndex(
                name: "IX_ContainerStyling_BackgroundId",
                table: "ContainerStyling");

            migrationBuilder.DropIndex(
                name: "IX_ContainerStyling_MarginId",
                table: "ContainerStyling");

            migrationBuilder.DropIndex(
                name: "IX_ContainerStyling_PaddingId",
                table: "ContainerStyling");

            migrationBuilder.DropColumn(
                name: "BackgroundId",
                table: "ContainerStyling");

            migrationBuilder.DropColumn(
                name: "MarginId",
                table: "ContainerStyling");

            migrationBuilder.DropColumn(
                name: "PaddingId",
                table: "ContainerStyling");

            migrationBuilder.RenameColumn(
                name: "StylingId",
                table: "GridRows",
                newName: "StyleId");

            migrationBuilder.RenameIndex(
                name: "IX_GridRows_StylingId",
                table: "GridRows",
                newName: "IX_GridRows_StyleId");

            migrationBuilder.RenameColumn(
                name: "StylingId",
                table: "GridColumns",
                newName: "ContainerStylingId");

            migrationBuilder.RenameIndex(
                name: "IX_GridColumns_StylingId",
                table: "GridColumns",
                newName: "IX_GridColumns_ContainerStylingId");

            migrationBuilder.AddColumn<int>(
                name: "StyleId",
                table: "ContainerStyling",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Style",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BackgroundId = table.Column<int>(type: "integer", nullable: true),
                    MarginId = table.Column<int>(type: "integer", nullable: true),
                    PaddingId = table.Column<int>(type: "integer", nullable: true)
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
                name: "IX_ContainerStyling_StyleId",
                table: "ContainerStyling",
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
                name: "FK_ContainerStyling_Style_StyleId",
                table: "ContainerStyling",
                column: "StyleId",
                principalTable: "Style",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GridColumns_ContainerStyling_ContainerStylingId",
                table: "GridColumns",
                column: "ContainerStylingId",
                principalTable: "ContainerStyling",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GridRows_Style_StyleId",
                table: "GridRows",
                column: "StyleId",
                principalTable: "Style",
                principalColumn: "Id");
        }
    }
}
