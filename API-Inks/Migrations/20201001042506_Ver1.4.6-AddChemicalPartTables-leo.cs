using Microsoft.EntityFrameworkCore.Migrations;

namespace INK_API.Migrations
{
    public partial class Ver146AddChemicalPartTablesleo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScheduleID",
                table: "Parts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScheduleID",
                table: "Parts");
        }
    }
}
