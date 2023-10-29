using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class pages19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridColumns_Component_ComponentId",
                table: "GridColumns");

            migrationBuilder.AlterColumn<int>(
                name: "ComponentId",
                table: "GridColumns",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_GridColumns_Component_ComponentId",
                table: "GridColumns",
                column: "ComponentId",
                principalTable: "Component",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridColumns_Component_ComponentId",
                table: "GridColumns");

            migrationBuilder.AlterColumn<int>(
                name: "ComponentId",
                table: "GridColumns",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GridColumns_Component_ComponentId",
                table: "GridColumns",
                column: "ComponentId",
                principalTable: "Component",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
