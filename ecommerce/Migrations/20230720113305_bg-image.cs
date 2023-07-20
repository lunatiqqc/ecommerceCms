using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class bgimage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Background_ImageFiles_BackgroundImageId",
                table: "Background");

            migrationBuilder.DropColumn(
                name: "ObjectFit",
                table: "Background");

            migrationBuilder.RenameColumn(
                name: "BackgroundImageId",
                table: "Background",
                newName: "ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Background_BackgroundImageId",
                table: "Background",
                newName: "IX_Background_ImageId");

            migrationBuilder.CreateTable(
                name: "ImageStyle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ObjectFit = table.Column<string>(type: "text", nullable: true),
                    BackgroundImageId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageStyle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageStyle_ImageFiles_BackgroundImageId",
                        column: x => x.BackgroundImageId,
                        principalTable: "ImageFiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageStyle_BackgroundImageId",
                table: "ImageStyle",
                column: "BackgroundImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Background_ImageStyle_ImageId",
                table: "Background",
                column: "ImageId",
                principalTable: "ImageStyle",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Background_ImageStyle_ImageId",
                table: "Background");

            migrationBuilder.DropTable(
                name: "ImageStyle");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Background",
                newName: "BackgroundImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Background_ImageId",
                table: "Background",
                newName: "IX_Background_BackgroundImageId");

            migrationBuilder.AddColumn<string>(
                name: "ObjectFit",
                table: "Background",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Background_ImageFiles_BackgroundImageId",
                table: "Background",
                column: "BackgroundImageId",
                principalTable: "ImageFiles",
                principalColumn: "Id");
        }
    }
}
