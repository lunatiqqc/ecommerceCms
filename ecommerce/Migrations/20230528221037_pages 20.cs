using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class pages20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageComponent",
                table: "GridColumns");

            migrationBuilder.DropColumn(
                name: "TextComponentId",
                table: "GridColumns");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageComponent",
                table: "GridColumns",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TextComponentId",
                table: "GridColumns",
                type: "integer",
                nullable: true);
        }
    }
}
