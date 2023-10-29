using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class pages5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Components",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Components",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Components",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Components");
        }
    }
}
