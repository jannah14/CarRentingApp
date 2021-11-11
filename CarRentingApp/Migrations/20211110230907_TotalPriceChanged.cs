using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentingApp.Migrations
{
    public partial class TotalPriceChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "TotalPrice",
                table: "Rentals",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "Rentals",
                type: "float",
                nullable: false,
                oldClrType: typeof(float));
        }
    }
}
