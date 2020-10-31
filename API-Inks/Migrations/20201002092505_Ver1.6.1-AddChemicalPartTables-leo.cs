using Microsoft.EntityFrameworkCore.Migrations;

namespace INK_API.Migrations
{
    public partial class Ver161AddChemicalPartTablesleo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_SchedulesUpdates_SchedulesUpdatesID",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Parts_SchedulesUpdatesID",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "SchedulesUpdatesID",
                table: "Parts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SchedulesUpdatesID",
                table: "Parts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parts_SchedulesUpdatesID",
                table: "Parts",
                column: "SchedulesUpdatesID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_SchedulesUpdates_SchedulesUpdatesID",
                table: "Parts",
                column: "SchedulesUpdatesID",
                principalTable: "SchedulesUpdates",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
