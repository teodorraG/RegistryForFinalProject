using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistryForFinalProject.Migrations
{
    public partial class Registry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegistryId",
                table: "Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Registries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Location = table.Column<string>(nullable: false),
                    DateOfEvent = table.Column<DateTime>(nullable: false),
                    RegistryType = table.Column<int>(nullable: false),
                    AccountId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registries_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_RegistryId",
                table: "Items",
                column: "RegistryId");

            migrationBuilder.CreateIndex(
                name: "IX_Registries_AccountId",
                table: "Registries",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Registries_RegistryId",
                table: "Items",
                column: "RegistryId",
                principalTable: "Registries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Registries_RegistryId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "Registries");

            migrationBuilder.DropIndex(
                name: "IX_Items_RegistryId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "RegistryId",
                table: "Items");
        }
    }
}
