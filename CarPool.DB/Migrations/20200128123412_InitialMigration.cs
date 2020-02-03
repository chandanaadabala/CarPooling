using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarPool.DB.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StopOvers",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StopOvers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    MobileNum = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Rides",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    UserID = table.Column<string>(nullable: false),
                    CarName = table.Column<string>(nullable: false),
                    NumberPlate = table.Column<string>(nullable: false),
                    Boarding = table.Column<string>(nullable: false),
                    Dropping = table.Column<string>(nullable: false),
                    Seats = table.Column<int>(nullable: false),
                    NumOfStopOvers = table.Column<int>(nullable: false),
                    BoardingDateTime = table.Column<DateTime>(nullable: false),
                    DroppingDateTime = table.Column<DateTime>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rides", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Rides_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    RideID = table.Column<string>(nullable: false),
                    UserID = table.Column<string>(nullable: false),
                    Boarding = table.Column<string>(nullable: false),
                    Dropping = table.Column<string>(nullable: false),
                    Seats = table.Column<int>(nullable: false),
                    Approval = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Bookings_Rides_RideID",
                        column: x => x.RideID,
                        principalTable: "Rides",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RideID",
                table: "Bookings",
                column: "RideID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserID",
                table: "Bookings",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_UserID",
                table: "Rides",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "StopOvers");

            migrationBuilder.DropTable(
                name: "Rides");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
