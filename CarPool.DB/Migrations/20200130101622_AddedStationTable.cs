using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarPool.DB.Migrations
{
    public partial class AddedStationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StopOvers");

            migrationBuilder.DropColumn(
                name: "CarName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NumberPlate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Boarding",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "BoardingDateTime",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "Dropping",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "DroppingDateTime",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "Seats",
                table: "Rides");

            migrationBuilder.RenameIndex(
                name: "IX_Rides_UserID",
                table: "Rides",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_UserID",
                table: "Bookings",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_RideID",
                table: "Bookings",
                newName: "RideID");

            migrationBuilder.AddColumn<int>(
                name: "AvailableSeats",
                table: "Rides",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerKM",
                table: "Rides",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    UserID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    NumberPlate = table.Column<string>(nullable: false),
                    Seats = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cars_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Station",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    RideID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    Distance = table.Column<double>(nullable: false),
                    AvailableSeats = table.Column<int>(nullable: false),
                    OrderNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Station", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Station_Rides_RideID",
                        column: x => x.RideID,
                        principalTable: "Rides",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UserID",
                table: "Cars",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "RideID",
                table: "Station",
                column: "RideID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Station");

            migrationBuilder.DropColumn(
                name: "AvailableSeats",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "PricePerKM",
                table: "Rides");

            migrationBuilder.RenameIndex(
                name: "UserID",
                table: "Rides",
                newName: "IX_Rides_UserID");

            migrationBuilder.RenameIndex(
                name: "UserID",
                table: "Bookings",
                newName: "IX_Bookings_UserID");

            migrationBuilder.RenameIndex(
                name: "RideID",
                table: "Bookings",
                newName: "IX_Bookings_RideID");

            migrationBuilder.AddColumn<string>(
                name: "CarName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumberPlate",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Boarding",
                table: "Rides",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "BoardingDateTime",
                table: "Rides",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Dropping",
                table: "Rides",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DroppingDateTime",
                table: "Rides",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Rides",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Seats",
                table: "Rides",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StopOvers",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AvailableSeats = table.Column<int>(type: "int", nullable: false),
                    Distance = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RideID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StopOvers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StopOvers_Rides_RideID",
                        column: x => x.RideID,
                        principalTable: "Rides",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "1",
                columns: new[] { "CarName", "NumberPlate" },
                values: new object[] { "Hyundai", "MH0237" });

            migrationBuilder.CreateIndex(
                name: "IX_StopOvers_RideID",
                table: "StopOvers",
                column: "RideID");
        }
    }
}
