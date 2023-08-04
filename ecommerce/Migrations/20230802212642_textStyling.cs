using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class textStyling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TextFormats",
                table: "TextContainerStyling");

            migrationBuilder.AlterColumn<int>(
                name: "FontSizeClassOptions",
                table: "TextContainerStyling",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ElementFormats",
                table: "TextContainerStyling",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "TextFormatsId",
                table: "TextContainerStyling",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TextFormats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Bold = table.Column<string>(type: "text", nullable: true),
                    Italic = table.Column<string>(type: "text", nullable: true),
                    Underline = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextFormats", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TextContainerStyling_TextFormatsId",
                table: "TextContainerStyling",
                column: "TextFormatsId");

            migrationBuilder.AddForeignKey(
                name: "FK_TextContainerStyling_TextFormats_TextFormatsId",
                table: "TextContainerStyling",
                column: "TextFormatsId",
                principalTable: "TextFormats",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TextContainerStyling_TextFormats_TextFormatsId",
                table: "TextContainerStyling");

            migrationBuilder.DropTable(
                name: "TextFormats");

            migrationBuilder.DropIndex(
                name: "IX_TextContainerStyling_TextFormatsId",
                table: "TextContainerStyling");

            migrationBuilder.DropColumn(
                name: "TextFormatsId",
                table: "TextContainerStyling");

            migrationBuilder.AlterColumn<int>(
                name: "FontSizeClassOptions",
                table: "TextContainerStyling",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ElementFormats",
                table: "TextContainerStyling",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TextFormats",
                table: "TextContainerStyling",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
