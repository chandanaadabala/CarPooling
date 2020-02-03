using Microsoft.EntityFrameworkCore.Migrations;

namespace CarPool.DB.Migrations
{
    public partial class UpdatedUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarName",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "NumberPlate",
                table: "Rides");

            migrationBuilder.AddColumn<string>(
                name: "CarName",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumberPlate",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NumberPlate",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "CarName",
                table: "Rides",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NumberPlate",
                table: "Rides",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
