using Microsoft.EntityFrameworkCore.Migrations;

namespace INK_API.Migrations
{
    public partial class Ver120UpdateTablesleo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inks_Processes_ProcessesID",
                table: "Inks");

            migrationBuilder.DropIndex(
                name: "IX_Inks_ProcessesID",
                table: "Inks");

            migrationBuilder.DropColumn(
                name: "Process",
                table: "Inks");

            migrationBuilder.DropColumn(
                name: "ProcessesID",
                table: "Inks");

            migrationBuilder.AddColumn<int>(
                name: "ProcessID",
                table: "Inks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Inks_ProcessID",
                table: "Inks",
                column: "ProcessID");

            migrationBuilder.AddForeignKey(
                name: "FK_Inks_Processes_ProcessID",
                table: "Inks",
                column: "ProcessID",
                principalTable: "Processes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inks_Processes_ProcessID",
                table: "Inks");

            migrationBuilder.DropIndex(
                name: "IX_Inks_ProcessID",
                table: "Inks");

            migrationBuilder.DropColumn(
                name: "ProcessID",
                table: "Inks");

            migrationBuilder.AddColumn<string>(
                name: "Process",
                table: "Inks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProcessesID",
                table: "Inks",
                type: "int",
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
    }
}
