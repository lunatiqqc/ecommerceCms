using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class textStylingComponent2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TextContainerStyling_TextContainerStyling_TextStylingId",
                table: "TextContainerStyling");

            migrationBuilder.DropIndex(
                name: "IX_TextContainerStyling_TextStylingId",
                table: "TextContainerStyling");

            migrationBuilder.DropColumn(
                name: "TextStylingId",
                table: "TextContainerStyling");

            migrationBuilder.AddColumn<int>(
                name: "TextStylingId",
                table: "TextComponents",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TextComponents_TextStylingId",
                table: "TextComponents",
                column: "TextStylingId");

            migrationBuilder.AddForeignKey(
                name: "FK_TextComponents_TextContainerStyling_TextStylingId",
                table: "TextComponents",
                column: "TextStylingId",
                principalTable: "TextContainerStyling",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TextComponents_TextContainerStyling_TextStylingId",
                table: "TextComponents");

            migrationBuilder.DropIndex(
                name: "IX_TextComponents_TextStylingId",
                table: "TextComponents");

            migrationBuilder.DropColumn(
                name: "TextStylingId",
                table: "TextComponents");

            migrationBuilder.AddColumn<int>(
                name: "TextStylingId",
                table: "TextContainerStyling",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TextContainerStyling_TextStylingId",
                table: "TextContainerStyling",
                column: "TextStylingId");

            migrationBuilder.AddForeignKey(
                name: "FK_TextContainerStyling_TextContainerStyling_TextStylingId",
                table: "TextContainerStyling",
                column: "TextStylingId",
                principalTable: "TextContainerStyling",
                principalColumn: "Id");
        }
    }
}
