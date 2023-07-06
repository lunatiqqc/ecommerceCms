using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class testpagenestedlistnull2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridColumns_Component_ComponentId",
                table: "GridColumns");

            migrationBuilder.DropIndex(
                name: "IX_GridColumns_ComponentId",
                table: "GridColumns");

            migrationBuilder.DropColumn(
                name: "ComponentId",
                table: "GridColumns");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComponentId",
                table: "GridColumns",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GridColumns_ComponentId",
                table: "GridColumns",
                column: "ComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_GridColumns_Component_ComponentId",
                table: "GridColumns",
                column: "ComponentId",
                principalTable: "Component",
                principalColumn: "Id");
        }
    }
}
