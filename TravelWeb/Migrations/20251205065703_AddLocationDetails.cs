using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddLocationDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChiTiet",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiaChi",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GiaCa",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GioCua",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "Locations",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoDienThoai",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChiTiet",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "DiaChi",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "GiaCa",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "GioCua",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "SoDienThoai",
                table: "Locations");
        }
    }
}
