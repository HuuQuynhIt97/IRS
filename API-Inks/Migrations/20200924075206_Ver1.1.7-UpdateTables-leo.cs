using Microsoft.EntityFrameworkCore.Migrations;

namespace INK_API.Migrations
{
    public partial class Ver117UpdateTablesleo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProcessID",
                table: "Supplier",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ObjectID",
                table: "Parts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProcessID",
                table: "InkTblObjects",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcessID",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "ObjectID",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "ProcessID",
                table: "InkTblObjects");
        }
    }
}
