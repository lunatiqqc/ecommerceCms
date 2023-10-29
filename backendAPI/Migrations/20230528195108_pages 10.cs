using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class pages10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridColumns_Components_ComponentId",
                table: "GridColumns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Components",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "Discriminator2",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Components");

            migrationBuilder.RenameTable(
                name: "Components",
                newName: "Component");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Component",
                table: "Component",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ImageComponents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageComponents_Component_Id",
                        column: x => x.Id,
                        principalTable: "Component",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TextComponents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextComponents_Component_Id",
                        column: x => x.Id,
                        principalTable: "Component",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_GridColumns_Component_ComponentId",
                table: "GridColumns",
                column: "ComponentId",
                principalTable: "Component",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GridColumns_Component_ComponentId",
                table: "GridColumns");

            migrationBuilder.DropTable(
                name: "ImageComponents");

            migrationBuilder.DropTable(
                name: "TextComponents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Component",
                table: "Component");

            migrationBuilder.RenameTable(
                name: "Component",
                newName: "Components");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator2",
                table: "Components",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Components",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Components",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Components",
                table: "Components",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GridColumns_Components_ComponentId",
                table: "GridColumns",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
