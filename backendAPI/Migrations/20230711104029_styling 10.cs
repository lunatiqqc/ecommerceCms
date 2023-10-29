using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class styling10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridColumns_Style_StyleId",
                table: "GridColumns");

            migrationBuilder.DropForeignKey(
                name: "FK_GridRows_Style_StyleId",
                table: "GridRows");

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
                name: "BaseStylingId",
                table: "GridRows",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BaseStylingId",
                table: "GridColumns",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BaseStyling",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StyleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseStyling", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseStyling_Style_StyleId",
                        column: x => x.StyleId,
                        principalTable: "Style",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GridRows_BaseStylingId",
                table: "GridRows",
                column: "BaseStylingId");

            migrationBuilder.CreateIndex(
                name: "IX_GridColumns_BaseStylingId",
                table: "GridColumns",
                column: "BaseStylingId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseStyling_StyleId",
                table: "BaseStyling",
                column: "StyleId");

            migrationBuilder.AddForeignKey(
                name: "FK_GridColumns_BaseStyling_BaseStylingId",
                table: "GridColumns",
                column: "BaseStylingId",
                principalTable: "BaseStyling",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GridRows_BaseStyling_BaseStylingId",
                table: "GridRows",
                column: "BaseStylingId",
                principalTable: "BaseStyling",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridColumns_BaseStyling_BaseStylingId",
                table: "GridColumns");

            migrationBuilder.DropForeignKey(
                name: "FK_GridRows_BaseStyling_BaseStylingId",
                table: "GridRows");

            migrationBuilder.DropTable(
                name: "BaseStyling");

            migrationBuilder.DropIndex(
                name: "IX_GridRows_BaseStylingId",
                table: "GridRows");

            migrationBuilder.DropIndex(
                name: "IX_GridColumns_BaseStylingId",
                table: "GridColumns");

            migrationBuilder.DropColumn(
                name: "BaseStylingId",
                table: "GridRows");

            migrationBuilder.DropColumn(
                name: "BaseStylingId",
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

            migrationBuilder.CreateIndex(
                name: "IX_GridRows_StyleId",
                table: "GridRows",
                column: "StyleId");

            migrationBuilder.CreateIndex(
                name: "IX_GridColumns_StyleId",
                table: "GridColumns",
                column: "StyleId");

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
    }
}
