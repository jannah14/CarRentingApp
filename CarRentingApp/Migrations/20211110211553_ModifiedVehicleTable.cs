using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentingApp.Migrations
{
    public partial class ModifiedVehicleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Vehicles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
