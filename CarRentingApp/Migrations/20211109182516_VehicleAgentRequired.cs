using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentingApp.Migrations
{
    public partial class VehicleAgentRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_AspNetUsers_AgentId",
                table: "Vehicles");

            migrationBuilder.AlterColumn<string>(
                name: "AgentId",
                table: "Vehicles",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_AspNetUsers_AgentId",
                table: "Vehicles",
                column: "AgentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_AspNetUsers_AgentId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "ItemsInStock",
                table: "Vehicles");

            migrationBuilder.AlterColumn<string>(
                name: "AgentId",
                table: "Vehicles",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_AspNetUsers_AgentId",
                table: "Vehicles",
                column: "AgentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
