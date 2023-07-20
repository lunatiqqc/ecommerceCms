using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class globalcontent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GlobalFooters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GridContentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalFooters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GlobalFooters_GridContent_GridContentId",
                        column: x => x.GridContentId,
                        principalTable: "GridContent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GlobalHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GridContentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GlobalHeaders_GridContent_GridContentId",
                        column: x => x.GridContentId,
                        principalTable: "GridContent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GlobalFooters_GridContentId",
                table: "GlobalFooters",
                column: "GridContentId");

            migrationBuilder.CreateIndex(
                name: "IX_GlobalHeaders_GridContentId",
                table: "GlobalHeaders",
                column: "GridContentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlobalFooters");

            migrationBuilder.DropTable(
                name: "GlobalHeaders");
        }
    }
}
