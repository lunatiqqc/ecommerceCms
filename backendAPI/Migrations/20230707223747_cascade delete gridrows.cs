using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class cascadedeletegridrows : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridRows_GridContent_GridContentId",
                table: "GridRows");

            migrationBuilder.AddForeignKey(
                name: "FK_GridRows_GridContent_GridContentId",
                table: "GridRows",
                column: "GridContentId",
                principalTable: "GridContent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridRows_GridContent_GridContentId",
                table: "GridRows");

            migrationBuilder.AddForeignKey(
                name: "FK_GridRows_GridContent_GridContentId",
                table: "GridRows",
                column: "GridContentId",
                principalTable: "GridContent",
                principalColumn: "Id");
        }
    }
}
