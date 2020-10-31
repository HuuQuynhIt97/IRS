using Microsoft.EntityFrameworkCore.Migrations;

namespace INK_API.Migrations
{
    public partial class Ver157AddChemicalPartTablesleo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SchedulesUpdateID",
                table: "Parts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parts_SchedulesUpdateID",
                table: "Parts",
                column: "SchedulesUpdateID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_SchedulesUpdates_SchedulesUpdateID",
                table: "Parts",
                column: "SchedulesUpdateID",
                principalTable: "SchedulesUpdates",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_SchedulesUpdates_SchedulesUpdateID",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Parts_SchedulesUpdateID",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "SchedulesUpdateID",
                table: "Parts");
        }
    }
}
