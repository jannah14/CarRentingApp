using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentingApp.Migrations
{
    public partial class RemoveItemsAvailableFromVehicles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemsAvailable",
                table: "Vehicles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemsAvailable",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
