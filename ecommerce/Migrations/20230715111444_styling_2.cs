using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class styling_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridRows_ContainerStyling_ContainerStylingId",
                table: "GridRows");

            migrationBuilder.RenameColumn(
                name: "ContainerStylingId",
                table: "GridRows",
                newName: "StyleId");

            migrationBuilder.RenameIndex(
                name: "IX_GridRows_ContainerStylingId",
                table: "GridRows",
                newName: "IX_GridRows_StyleId");

            migrationBuilder.AddForeignKey(
                name: "FK_GridRows_Style_StyleId",
                table: "GridRows",
                column: "StyleId",
                principalTable: "Style",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridRows_Style_StyleId",
                table: "GridRows");

            migrationBuilder.RenameColumn(
                name: "StyleId",
                table: "GridRows",
                newName: "ContainerStylingId");

            migrationBuilder.RenameIndex(
                name: "IX_GridRows_StyleId",
                table: "GridRows",
                newName: "IX_GridRows_ContainerStylingId");

            migrationBuilder.AddForeignKey(
                name: "FK_GridRows_ContainerStyling_ContainerStylingId",
                table: "GridRows",
                column: "ContainerStylingId",
                principalTable: "ContainerStyling",
                principalColumn: "Id");
        }
    }
}
