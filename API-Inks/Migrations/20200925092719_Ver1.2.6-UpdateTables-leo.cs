using Microsoft.EntityFrameworkCore.Migrations;

namespace INK_API.Migrations
{
    public partial class Ver126UpdateTablesleo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ArtProcessID",
                table: "Schedules",
                column: "ArtProcessID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_ArtProcesses_ArtProcessID",
                table: "Schedules",
                column: "ArtProcessID",
                principalTable: "ArtProcesses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_ArtProcesses_ArtProcessID",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_ArtProcessID",
                table: "Schedules");
        }
    }
}
