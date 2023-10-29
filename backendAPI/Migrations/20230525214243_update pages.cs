using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class updatepages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSystemPage",
                table: "Pages");

            migrationBuilder.AlterColumn<bool>(
                name: "VisibleInMenu",
                table: "Pages",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "VisibleInMenu",
                table: "Pages",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSystemPage",
                table: "Pages",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
