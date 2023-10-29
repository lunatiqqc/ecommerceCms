using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class bgimage4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Background_ImageStyle_BackgroudImageId",
                table: "Background");

            migrationBuilder.RenameColumn(
                name: "BackgroudImageId",
                table: "Background",
                newName: "BackgroundImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Background_BackgroudImageId",
                table: "Background",
                newName: "IX_Background_BackgroundImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Background_ImageStyle_BackgroundImageId",
                table: "Background",
                column: "BackgroundImageId",
                principalTable: "ImageStyle",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Background_ImageStyle_BackgroundImageId",
                table: "Background");

            migrationBuilder.RenameColumn(
                name: "BackgroundImageId",
                table: "Background",
                newName: "BackgroudImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Background_BackgroundImageId",
                table: "Background",
                newName: "IX_Background_BackgroudImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Background_ImageStyle_BackgroudImageId",
                table: "Background",
                column: "BackgroudImageId",
                principalTable: "ImageStyle",
                principalColumn: "Id");
        }
    }
}
