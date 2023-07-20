using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class uploadfiles5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "AspectRatio",
                table: "Files",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "Files",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AspectRatio",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "Files");
        }
    }
}
