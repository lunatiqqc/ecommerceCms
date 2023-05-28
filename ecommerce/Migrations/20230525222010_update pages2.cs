using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class updatepages2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pages_Pages_ParentId",
                table: "Pages");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "Pages",
                newName: "PageId");

            migrationBuilder.RenameIndex(
                name: "IX_Pages_ParentId",
                table: "Pages",
                newName: "IX_Pages_PageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_Pages_PageId",
                table: "Pages",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pages_Pages_PageId",
                table: "Pages");

            migrationBuilder.RenameColumn(
                name: "PageId",
                table: "Pages",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Pages_PageId",
                table: "Pages",
                newName: "IX_Pages_ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_Pages_ParentId",
                table: "Pages",
                column: "ParentId",
                principalTable: "Pages",
                principalColumn: "Id");
        }
    }
}
