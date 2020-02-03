using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarPool.DB.Migrations
{
    public partial class AddedSoftDeletionAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableSeats",
                table: "Station");

            migrationBuilder.DropColumn(
                name: "Boarding",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Dropping",
                table: "Bookings");

            migrationBuilder.AddColumn<DateTime>(
                name: "RowDeletedOn",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RowModifiedOn",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RowDeletedOn",
                table: "Station",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RowModifiedOn",
                table: "Station",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RowDeletedOn",
                table: "Rides",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RowModifiedOn",
                table: "Rides",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RowDeletedOn",
                table: "Cars",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RowModifiedOn",
                table: "Cars",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "BoardingID",
                table: "Bookings",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DroppingID",
                table: "Bookings",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RowDeletedOn",
                table: "Bookings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "RowModifiedOn",
                table: "Bookings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BoardingID",
                table: "Bookings",
                column: "BoardingID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_DroppingID",
                table: "Bookings",
                column: "DroppingID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Station_BoardingID",
                table: "Bookings",
                column: "BoardingID",
                principalTable: "Station",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Station_DroppingID",
                table: "Bookings",
                column: "DroppingID",
                principalTable: "Station",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Station_BoardingID",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Station_DroppingID",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_BoardingID",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_DroppingID",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "RowDeletedOn",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RowModifiedOn",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RowDeletedOn",
                table: "Station");

            migrationBuilder.DropColumn(
                name: "RowModifiedOn",
                table: "Station");

            migrationBuilder.DropColumn(
                name: "RowDeletedOn",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "RowModifiedOn",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "RowDeletedOn",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "RowModifiedOn",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "BoardingID",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "DroppingID",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "RowDeletedOn",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "RowModifiedOn",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "AvailableSeats",
                table: "Station",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Boarding",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Dropping",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
