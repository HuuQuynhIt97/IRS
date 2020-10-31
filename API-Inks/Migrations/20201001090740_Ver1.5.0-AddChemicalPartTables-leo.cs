using Microsoft.EntityFrameworkCore.Migrations;

namespace INK_API.Migrations
{
    public partial class Ver150AddChemicalPartTablesleo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Processes_ProcessID",
                table: "Schedules");

      

            migrationBuilder.DropColumn(
                name: "ProcessID",
                table: "Schedules");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProcessID",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

          
            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Processes_ProcessID",
                table: "Schedules",
                column: "ProcessID",
                principalTable: "Processes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
