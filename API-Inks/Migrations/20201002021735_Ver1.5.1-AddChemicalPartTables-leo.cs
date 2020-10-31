using Microsoft.EntityFrameworkCore.Migrations;

namespace INK_API.Migrations
{
    public partial class Ver151AddChemicalPartTablesleo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArtProcessID",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "ProcessID",
                table: "Parts");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Parts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Parts");

            migrationBuilder.AddColumn<int>(
                name: "ArtProcessID",
                table: "Parts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProcessID",
                table: "Parts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
