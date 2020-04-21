using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistryForFinalProject.Migrations
{
    public partial class RegistryItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Registries_RegistryId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Registries_Accounts_AccountId",
                table: "Registries");

            migrationBuilder.DropIndex(
                name: "IX_Items_RegistryId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "RegistryId",
                table: "Items");

            migrationBuilder.CreateTable(
                name: "RegistryItems",
                columns: table => new
                {
                    ItemId = table.Column<int>(nullable: false),
                    RegistryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistryItems", x => new { x.RegistryId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_RegistryItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistryItems_Registries_RegistryId",
                        column: x => x.RegistryId,
                        principalTable: "Registries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistryItems_ItemId",
                table: "RegistryItems",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registries_Accounts_AccountId",
                table: "Registries",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registries_Accounts_AccountId",
                table: "Registries");

            migrationBuilder.DropTable(
                name: "RegistryItems");

            migrationBuilder.AddColumn<int>(
                name: "RegistryId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Items_RegistryId",
                table: "Items",
                column: "RegistryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Registries_RegistryId",
                table: "Items",
                column: "RegistryId",
                principalTable: "Registries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registries_Accounts_AccountId",
                table: "Registries",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
