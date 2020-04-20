using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistryForFinalProject.Migrations
{
    public partial class updatedOrdersWithPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EasyPayNumber",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethod",
                table: "Orders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EasyPayNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Orders");
        }
    }
}
