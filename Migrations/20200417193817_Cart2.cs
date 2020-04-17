using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistryForFinalProject.Migrations
{
    public partial class Cart2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Items",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(88)",
                oldMaxLength: 88);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Items",
                type: "nvarchar(88)",
                maxLength: 88,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
