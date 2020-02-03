using Microsoft.EntityFrameworkCore.Migrations;

namespace CarPool.DB.Migrations
{
    public partial class InitialMigrationV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvailableSeats",
                table: "StopOvers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RideID",
                table: "StopOvers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_StopOvers_RideID",
                table: "StopOvers",
                column: "RideID");

            migrationBuilder.AddForeignKey(
                name: "FK_StopOvers_Rides_RideID",
                table: "StopOvers",
                column: "RideID",
                principalTable: "Rides",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StopOvers_Rides_RideID",
                table: "StopOvers");

            migrationBuilder.DropIndex(
                name: "IX_StopOvers_RideID",
                table: "StopOvers");

            migrationBuilder.DropColumn(
                name: "AvailableSeats",
                table: "StopOvers");

            migrationBuilder.DropColumn(
                name: "RideID",
                table: "StopOvers");
        }
    }
}
