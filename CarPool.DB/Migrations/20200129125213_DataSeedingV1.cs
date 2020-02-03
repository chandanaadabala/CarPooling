using Microsoft.EntityFrameworkCore.Migrations;

namespace CarPool.DB.Migrations
{
    public partial class DataSeedingV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "StopOvers");

            migrationBuilder.AddColumn<double>(
                name: "Distance",
                table: "StopOvers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "CarName", "Email", "MobileNum", "Name", "NumberPlate", "Password" },
                values: new object[] { "1", "Hyundai", "sam@gmail.com", "0987654321", "SamYoung", "MH0237", "qazwsxedc" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: "1");

            migrationBuilder.DropColumn(
                name: "Distance",
                table: "StopOvers");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "StopOvers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
