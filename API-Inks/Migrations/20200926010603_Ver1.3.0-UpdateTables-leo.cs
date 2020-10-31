using Microsoft.EntityFrameworkCore.Migrations;

namespace INK_API.Migrations
{
    public partial class Ver130UpdateTablesleo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_InkTblObjects_InkTblObjectID",
                table: "Schedules");

            migrationBuilder.AlterColumn<int>(
                name: "InkTblObjectID",
                table: "Schedules",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_InkTblObjects_InkTblObjectID",
                table: "Schedules",
                column: "InkTblObjectID",
                principalTable: "InkTblObjects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_InkTblObjects_InkTblObjectID",
                table: "Schedules");

            migrationBuilder.AlterColumn<int>(
                name: "InkTblObjectID",
                table: "Schedules",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_InkTblObjects_InkTblObjectID",
                table: "Schedules",
                column: "InkTblObjectID",
                principalTable: "InkTblObjects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
