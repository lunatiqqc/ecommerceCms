using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class bgimage2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Background_ImageStyle_ImageId",
                table: "Background");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Background",
                newName: "ImageStyleId");

            migrationBuilder.RenameIndex(
                name: "IX_Background_ImageId",
                table: "Background",
                newName: "IX_Background_ImageStyleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Background_ImageStyle_ImageStyleId",
                table: "Background",
                column: "ImageStyleId",
                principalTable: "ImageStyle",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Background_ImageStyle_ImageStyleId",
                table: "Background");

            migrationBuilder.RenameColumn(
                name: "ImageStyleId",
                table: "Background",
                newName: "ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Background_ImageStyleId",
                table: "Background",
                newName: "IX_Background_ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Background_ImageStyle_ImageId",
                table: "Background",
                column: "ImageId",
                principalTable: "ImageStyle",
                principalColumn: "Id");
        }
    }
}
