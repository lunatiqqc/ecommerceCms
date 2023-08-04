using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class textstylin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StylingId",
                table: "Component",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Component_StylingId",
                table: "Component",
                column: "StylingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Component_ContainerStyling_StylingId",
                table: "Component",
                column: "StylingId",
                principalTable: "ContainerStyling",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Component_ContainerStyling_StylingId",
                table: "Component");

            migrationBuilder.DropIndex(
                name: "IX_Component_StylingId",
                table: "Component");

            migrationBuilder.DropColumn(
                name: "StylingId",
                table: "Component");
        }
    }
}
