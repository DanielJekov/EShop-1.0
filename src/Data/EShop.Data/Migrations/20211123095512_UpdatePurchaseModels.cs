using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.Data.Migrations
{
    public partial class UpdatePurchaseModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "QuickPurchases",
                newName: "UserDescription");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Purchases",
                newName: "UserDescription");

            migrationBuilder.AddColumn<string>(
                name: "StatusDescription",
                table: "QuickPurchases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusDescription",
                table: "Purchases",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusDescription",
                table: "QuickPurchases");

            migrationBuilder.DropColumn(
                name: "StatusDescription",
                table: "Purchases");

            migrationBuilder.RenameColumn(
                name: "UserDescription",
                table: "QuickPurchases",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "UserDescription",
                table: "Purchases",
                newName: "Description");
        }
    }
}
