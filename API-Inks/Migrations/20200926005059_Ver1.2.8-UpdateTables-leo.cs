using Microsoft.EntityFrameworkCore.Migrations;

namespace INK_API.Migrations
{
    public partial class Ver128UpdateTablesleo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_InkTblObjects_ProcessID",
                table: "InkTblObjects",
                column: "ProcessID");

            migrationBuilder.AddForeignKey(
                name: "FK_InkTblObjects_Processes_ProcessID",
                table: "InkTblObjects",
                column: "ProcessID",
                principalTable: "Processes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InkTblObjects_Processes_ProcessID",
                table: "InkTblObjects");

            migrationBuilder.DropIndex(
                name: "IX_InkTblObjects_ProcessID",
                table: "InkTblObjects");
        }
    }
}
