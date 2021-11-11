using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentingApp.Migrations
{
    public partial class AlterTotalPriceRentals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_AspNetUsers_AppUserId",
                table: "Rentals");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Rentals",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_AspNetUsers_AppUserId",
                table: "Rentals",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_AspNetUsers_AppUserId",
                table: "Rentals");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Rentals",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_AspNetUsers_AppUserId",
                table: "Rentals",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
