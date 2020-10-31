using Microsoft.EntityFrameworkCore.Migrations;

namespace INK_API.Migrations
{
    public partial class Ver165AddTreatmentWayTablesleo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TreatmentWayID",
                table: "Glueses",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TreatmentWayID",
                table: "Glueses");
        }
    }
}
