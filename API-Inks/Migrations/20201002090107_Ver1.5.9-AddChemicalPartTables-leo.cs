using Microsoft.EntityFrameworkCore.Migrations;

namespace INK_API.Migrations
{
    public partial class Ver159AddChemicalPartTablesleo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchedulesUpdates_Parts_PartsID",
                table: "SchedulesUpdates");

            migrationBuilder.DropIndex(
                name: "IX_SchedulesUpdates_PartsID",
                table: "SchedulesUpdates");

            migrationBuilder.DropColumn(
                name: "PartsID",
                table: "SchedulesUpdates");

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

            migrationBuilder.AddColumn<int>(
                name: "PartsID",
                table: "SchedulesUpdates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SchedulesUpdates_PartsID",
                table: "SchedulesUpdates",
                column: "PartsID");

            migrationBuilder.AddForeignKey(
                name: "FK_SchedulesUpdates_Parts_PartsID",
                table: "SchedulesUpdates",
                column: "PartsID",
                principalTable: "Parts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
