using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class textstyling__2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "MenuComponents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    TextStylingId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuComponents_Component_Id",
                        column: x => x.Id,
                        principalTable: "Component",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuComponents_ContainerStyling_TextStylingId",
                        column: x => x.TextStylingId,
                        principalTable: "ContainerStyling",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuComponents_TextStylingId",
                table: "MenuComponents",
                column: "TextStylingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuComponents");

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
        }
    }
}
