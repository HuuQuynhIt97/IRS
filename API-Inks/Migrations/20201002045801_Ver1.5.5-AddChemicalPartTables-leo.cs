using Microsoft.EntityFrameworkCore.Migrations;

namespace INK_API.Migrations
{
    public partial class Ver155AddChemicalPartTablesleo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScheduleID",
                table: "ModelNos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ScheduleID",
                table: "ModelNames",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ScheduleID",
                table: "ArticleNos",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScheduleID",
                table: "ModelNos");

            migrationBuilder.DropColumn(
                name: "ScheduleID",
                table: "ModelNames");

            migrationBuilder.DropColumn(
                name: "ScheduleID",
                table: "ArticleNos");
        }
    }
}
