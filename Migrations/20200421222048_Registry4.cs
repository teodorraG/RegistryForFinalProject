using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistryForFinalProject.Migrations
{
    public partial class Registry4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Registry_RegistryId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Registry_Accounts_AccountId",
                table: "Registry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Registry",
                table: "Registry");

            migrationBuilder.RenameTable(
                name: "Registry",
                newName: "Registries");

            migrationBuilder.RenameIndex(
                name: "IX_Registry_AccountId",
                table: "Registries",
                newName: "IX_Registries_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Registries",
                table: "Registries",
                column: "Id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Registries_RegistryId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Registries_Accounts_AccountId",
                table: "Registries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Registries",
                table: "Registries");

            migrationBuilder.RenameTable(
                name: "Registries",
                newName: "Registry");

            migrationBuilder.RenameIndex(
                name: "IX_Registries_AccountId",
                table: "Registry",
                newName: "IX_Registry_AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Registry",
                table: "Registry",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Registry_RegistryId",
                table: "Items",
                column: "RegistryId",
                principalTable: "Registry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registry_Accounts_AccountId",
                table: "Registry",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
