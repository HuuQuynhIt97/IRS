using Microsoft.EntityFrameworkCore.Migrations;

namespace INK_API.Migrations
{
    public partial class Ver119UpdateTablesleo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProcessesID",
                table: "Inks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inks_ProcessesID",
                table: "Inks",
                column: "ProcessesID");

            migrationBuilder.AddForeignKey(
                name: "FK_Inks_Processes_ProcessesID",
                table: "Inks",
                column: "ProcessesID",
                principalTable: "Processes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inks_Processes_ProcessesID",
                table: "Inks");

            migrationBuilder.DropIndex(
                name: "IX_Inks_ProcessesID",
                table: "Inks");

            migrationBuilder.DropColumn(
                name: "ProcessesID",
                table: "Inks");
        }
    }
}
