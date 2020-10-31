using Microsoft.EntityFrameworkCore.Migrations;

namespace INK_API.Migrations
{
    public partial class Ver132UpdateTablesleo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Processes_ProcessID",
                table: "Schedules");

            migrationBuilder.AlterColumn<int>(
                name: "ProcessID",
                table: "Schedules",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Processes_ProcessID",
                table: "Schedules",
                column: "ProcessID",
                principalTable: "Processes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Processes_ProcessID",
                table: "Schedules");

            migrationBuilder.AlterColumn<int>(
                name: "ProcessID",
                table: "Schedules",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Processes_ProcessID",
                table: "Schedules",
                column: "ProcessID",
                principalTable: "Processes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
