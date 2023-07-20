using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class uploadfiles7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_FileFolders_FileFolderId",
                table: "Files");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Files",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "AspectRatio",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "Files");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "CmsFiles");

            migrationBuilder.RenameIndex(
                name: "IX_Files_FileFolderId",
                table: "CmsFiles",
                newName: "IX_CmsFiles_FileFolderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CmsFiles",
                table: "CmsFiles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ImageFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    AspectRatio = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageFiles_CmsFiles_Id",
                        column: x => x.Id,
                        principalTable: "CmsFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CmsFiles_FileFolders_FileFolderId",
                table: "CmsFiles",
                column: "FileFolderId",
                principalTable: "FileFolders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CmsFiles_FileFolders_FileFolderId",
                table: "CmsFiles");

            migrationBuilder.DropTable(
                name: "ImageFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CmsFiles",
                table: "CmsFiles");

            migrationBuilder.RenameTable(
                name: "CmsFiles",
                newName: "Files");

            migrationBuilder.RenameIndex(
                name: "IX_CmsFiles_FileFolderId",
                table: "Files",
                newName: "IX_Files_FileFolderId");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_FileFolders_FileFolderId",
                table: "Files",
                column: "FileFolderId",
                principalTable: "FileFolders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
