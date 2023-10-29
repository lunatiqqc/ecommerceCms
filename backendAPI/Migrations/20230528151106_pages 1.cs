using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class pages1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GridRows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PageId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GridRows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GridRows_Pages_PageId",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GridColumns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Width = table.Column<int>(type: "integer", nullable: false),
                    BaseComponentId = table.Column<int>(type: "integer", nullable: true),
                    ComponentId = table.Column<int>(type: "integer", nullable: false),
                    GridRowId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GridColumns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GridColumns_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GridColumns_GridRows_GridRowId",
                        column: x => x.GridRowId,
                        principalTable: "GridRows",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GridColumns_ComponentId",
                table: "GridColumns",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_GridColumns_GridRowId",
                table: "GridColumns",
                column: "GridRowId");

            migrationBuilder.CreateIndex(
                name: "IX_GridRows_PageId",
                table: "GridRows",
                column: "PageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GridColumns");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "GridRows");
        }
    }
}
