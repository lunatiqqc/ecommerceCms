using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class texteditor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "TextComponents",
                newName: "Html");

            migrationBuilder.RenameColumn(
                name: "DeltaFormat",
                table: "TextComponents",
                newName: "EditorState");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Html",
                table: "TextComponents",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "EditorState",
                table: "TextComponents",
                newName: "DeltaFormat");
        }
    }
}
