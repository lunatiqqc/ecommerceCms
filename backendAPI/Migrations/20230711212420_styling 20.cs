using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class styling20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridColumns_BaseStyling_BaseStylingId",
                table: "GridColumns");

            migrationBuilder.DropForeignKey(
                name: "FK_GridRows_BaseStyling_BaseStylingId",
                table: "GridRows");

            migrationBuilder.DropTable(
                name: "BaseStyling");

            migrationBuilder.RenameColumn(
                name: "BaseStylingId",
                table: "GridRows",
                newName: "ContainerStylingId");

            migrationBuilder.RenameIndex(
                name: "IX_GridRows_BaseStylingId",
                table: "GridRows",
                newName: "IX_GridRows_ContainerStylingId");

            migrationBuilder.RenameColumn(
                name: "BaseStylingId",
                table: "GridColumns",
                newName: "ContainerStylingId");

            migrationBuilder.RenameIndex(
                name: "IX_GridColumns_BaseStylingId",
                table: "GridColumns",
                newName: "IX_GridColumns_ContainerStylingId");

            migrationBuilder.CreateTable(
                name: "ContainerStyling",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StyleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContainerStyling", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContainerStyling_Style_StyleId",
                        column: x => x.StyleId,
                        principalTable: "Style",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContainerStyling_StyleId",
                table: "ContainerStyling",
                column: "StyleId");

            migrationBuilder.AddForeignKey(
                name: "FK_GridColumns_ContainerStyling_ContainerStylingId",
                table: "GridColumns",
                column: "ContainerStylingId",
                principalTable: "ContainerStyling",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GridRows_ContainerStyling_ContainerStylingId",
                table: "GridRows",
                column: "ContainerStylingId",
                principalTable: "ContainerStyling",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridColumns_ContainerStyling_ContainerStylingId",
                table: "GridColumns");

            migrationBuilder.DropForeignKey(
                name: "FK_GridRows_ContainerStyling_ContainerStylingId",
                table: "GridRows");

            migrationBuilder.DropTable(
                name: "ContainerStyling");

            migrationBuilder.RenameColumn(
                name: "ContainerStylingId",
                table: "GridRows",
                newName: "BaseStylingId");

            migrationBuilder.RenameIndex(
                name: "IX_GridRows_ContainerStylingId",
                table: "GridRows",
                newName: "IX_GridRows_BaseStylingId");

            migrationBuilder.RenameColumn(
                name: "ContainerStylingId",
                table: "GridColumns",
                newName: "BaseStylingId");

            migrationBuilder.RenameIndex(
                name: "IX_GridColumns_ContainerStylingId",
                table: "GridColumns",
                newName: "IX_GridColumns_BaseStylingId");

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
    }
}
