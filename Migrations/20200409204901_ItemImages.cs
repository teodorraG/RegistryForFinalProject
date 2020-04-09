using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistryForFinalProject.Migrations
{
    public partial class ItemImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Items");

            migrationBuilder.AddColumn<string>(
                name: "Image1",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image2",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image3",
                table: "Items",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image1",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Image2",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Image3",
                table: "Items");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
