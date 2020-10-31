using Microsoft.EntityFrameworkCore.Migrations;

namespace INK_API.Migrations
{
    public partial class Ver121UpdateTablesleo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Process",
                table: "Chemicals");

            migrationBuilder.AlterColumn<double>(
                name: "Unit",
                table: "Chemicals",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProcessID",
                table: "Chemicals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Chemicals_ProcessID",
                table: "Chemicals",
                column: "ProcessID");

            migrationBuilder.AddForeignKey(
                name: "FK_Chemicals_Processes_ProcessID",
                table: "Chemicals",
                column: "ProcessID",
                principalTable: "Processes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chemicals_Processes_ProcessID",
                table: "Chemicals");

            migrationBuilder.DropIndex(
                name: "IX_Chemicals_ProcessID",
                table: "Chemicals");

            migrationBuilder.DropColumn(
                name: "ProcessID",
                table: "Chemicals");

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "Chemicals",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<string>(
                name: "Process",
                table: "Chemicals",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
