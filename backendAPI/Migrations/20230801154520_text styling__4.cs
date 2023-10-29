using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class textstyling__4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuComponents_ContainerStyling_TextStylingId",
                table: "MenuComponents");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "ContainerStyling");

            migrationBuilder.DropColumn(
                name: "ElementFormats",
                table: "ContainerStyling");

            migrationBuilder.DropColumn(
                name: "FontSizeClassOptions",
                table: "ContainerStyling");

            migrationBuilder.DropColumn(
                name: "TextFormats",
                table: "ContainerStyling");

            migrationBuilder.CreateTable(
                name: "TextContainerStyling",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TextFormats = table.Column<int>(type: "integer", nullable: false),
                    ElementFormats = table.Column<int>(type: "integer", nullable: false),
                    FontSizeClassOptions = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextContainerStyling", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_MenuComponents_TextContainerStyling_TextStylingId",
                table: "MenuComponents",
                column: "TextStylingId",
                principalTable: "TextContainerStyling",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuComponents_TextContainerStyling_TextStylingId",
                table: "MenuComponents");

            migrationBuilder.DropTable(
                name: "TextContainerStyling");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "ContainerStyling",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ElementFormats",
                table: "ContainerStyling",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FontSizeClassOptions",
                table: "ContainerStyling",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TextFormats",
                table: "ContainerStyling",
                type: "integer",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuComponents_ContainerStyling_TextStylingId",
                table: "MenuComponents",
                column: "TextStylingId",
                principalTable: "ContainerStyling",
                principalColumn: "Id");
        }
    }
}
