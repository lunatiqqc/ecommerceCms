using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class updatepages4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pages_Pages_PageId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "IsRootPage",
                table: "Pages");

            migrationBuilder.RenameColumn(
                name: "PageId",
                table: "Pages",
                newName: "ParentPageId");

            migrationBuilder.RenameIndex(
                name: "IX_Pages_PageId",
                table: "Pages",
                newName: "IX_Pages_ParentPageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_Pages_ParentPageId",
                table: "Pages",
                column: "ParentPageId",
                principalTable: "Pages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pages_Pages_ParentPageId",
                table: "Pages");

            migrationBuilder.RenameColumn(
                name: "ParentPageId",
                table: "Pages",
                newName: "PageId");

            migrationBuilder.RenameIndex(
                name: "IX_Pages_ParentPageId",
                table: "Pages",
                newName: "IX_Pages_PageId");

            migrationBuilder.AddColumn<bool>(
                name: "IsRootPage",
                table: "Pages",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_Pages_PageId",
                table: "Pages",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id");
        }
    }
}
