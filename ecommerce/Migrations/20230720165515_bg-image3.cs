using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class bgimage3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Background_ImageStyle_ImageStyleId",
                table: "Background");

            migrationBuilder.DropForeignKey(
                name: "FK_ImageStyle_ImageFiles_BackgroundImageId",
                table: "ImageStyle");

            migrationBuilder.RenameColumn(
                name: "BackgroundImageId",
                table: "ImageStyle",
                newName: "ImageFileId");

            migrationBuilder.RenameIndex(
                name: "IX_ImageStyle_BackgroundImageId",
                table: "ImageStyle",
                newName: "IX_ImageStyle_ImageFileId");

            migrationBuilder.RenameColumn(
                name: "ImageStyleId",
                table: "Background",
                newName: "BackgroudImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Background_ImageStyleId",
                table: "Background",
                newName: "IX_Background_BackgroudImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Background_ImageStyle_BackgroudImageId",
                table: "Background",
                column: "BackgroudImageId",
                principalTable: "ImageStyle",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageStyle_ImageFiles_ImageFileId",
                table: "ImageStyle",
                column: "ImageFileId",
                principalTable: "ImageFiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Background_ImageStyle_BackgroudImageId",
                table: "Background");

            migrationBuilder.DropForeignKey(
                name: "FK_ImageStyle_ImageFiles_ImageFileId",
                table: "ImageStyle");

            migrationBuilder.RenameColumn(
                name: "ImageFileId",
                table: "ImageStyle",
                newName: "BackgroundImageId");

            migrationBuilder.RenameIndex(
                name: "IX_ImageStyle_ImageFileId",
                table: "ImageStyle",
                newName: "IX_ImageStyle_BackgroundImageId");

            migrationBuilder.RenameColumn(
                name: "BackgroudImageId",
                table: "Background",
                newName: "ImageStyleId");

            migrationBuilder.RenameIndex(
                name: "IX_Background_BackgroudImageId",
                table: "Background",
                newName: "IX_Background_ImageStyleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Background_ImageStyle_ImageStyleId",
                table: "Background",
                column: "ImageStyleId",
                principalTable: "ImageStyle",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageStyle_ImageFiles_BackgroundImageId",
                table: "ImageStyle",
                column: "BackgroundImageId",
                principalTable: "ImageFiles",
                principalColumn: "Id");
        }
    }
}
