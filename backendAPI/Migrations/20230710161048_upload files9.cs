using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class uploadfiles9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileFolders_FileFolders_ParentCategoryId",
                table: "FileFolders");

            migrationBuilder.RenameColumn(
                name: "ParentCategoryId",
                table: "FileFolders",
                newName: "ParentFolderId");

            migrationBuilder.RenameIndex(
                name: "IX_FileFolders_ParentCategoryId",
                table: "FileFolders",
                newName: "IX_FileFolders_ParentFolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileFolders_FileFolders_ParentFolderId",
                table: "FileFolders",
                column: "ParentFolderId",
                principalTable: "FileFolders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileFolders_FileFolders_ParentFolderId",
                table: "FileFolders");

            migrationBuilder.RenameColumn(
                name: "ParentFolderId",
                table: "FileFolders",
                newName: "ParentCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_FileFolders_ParentFolderId",
                table: "FileFolders",
                newName: "IX_FileFolders_ParentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileFolders_FileFolders_ParentCategoryId",
                table: "FileFolders",
                column: "ParentCategoryId",
                principalTable: "FileFolders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
