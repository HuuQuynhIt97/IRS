using Microsoft.EntityFrameworkCore.Migrations;

namespace INK_API.Migrations
{
    public partial class Ver125UpdateTablesleo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Schedules_PartID",
                table: "Schedules",
                column: "PartID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Parts_PartID",
                table: "Schedules",
                column: "PartID",
                principalTable: "Parts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Parts_PartID",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_PartID",
                table: "Schedules");
        }
    }
}
