using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class styling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Background",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BackgroundImageId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Background", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Background_ImageFiles_BackgroundImageId",
                        column: x => x.BackgroundImageId,
                        principalTable: "ImageFiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Margin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Top = table.Column<int>(type: "integer", nullable: true),
                    Right = table.Column<int>(type: "integer", nullable: true),
                    Bottom = table.Column<int>(type: "integer", nullable: true),
                    Left = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Margin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Padding",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Top = table.Column<int>(type: "integer", nullable: true),
                    Right = table.Column<int>(type: "integer", nullable: true),
                    Bottom = table.Column<int>(type: "integer", nullable: true),
                    Left = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Padding", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BaseStyling",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PaddingId = table.Column<int>(type: "integer", nullable: true),
                    MarginId = table.Column<int>(type: "integer", nullable: true),
                    BackgroundId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseStyling", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseStyling_Background_BackgroundId",
                        column: x => x.BackgroundId,
                        principalTable: "Background",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BaseStyling_Margin_MarginId",
                        column: x => x.MarginId,
                        principalTable: "Margin",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BaseStyling_Padding_PaddingId",
                        column: x => x.PaddingId,
                        principalTable: "Padding",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Background_BackgroundImageId",
                table: "Background",
                column: "BackgroundImageId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseStyling_BackgroundId",
                table: "BaseStyling",
                column: "BackgroundId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseStyling_MarginId",
                table: "BaseStyling",
                column: "MarginId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseStyling_PaddingId",
                table: "BaseStyling",
                column: "PaddingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseStyling");

            migrationBuilder.DropTable(
                name: "Background");

            migrationBuilder.DropTable(
                name: "Margin");

            migrationBuilder.DropTable(
                name: "Padding");
        }
    }
}
