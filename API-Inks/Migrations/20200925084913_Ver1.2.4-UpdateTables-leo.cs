using Microsoft.EntityFrameworkCore.Migrations;

namespace INK_API.Migrations
{
    public partial class Ver124UpdateTablesleo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BPFCEstablishes_Parts_PartID",
                table: "BPFCEstablishes");

            migrationBuilder.DropIndex(
                name: "IX_BPFCEstablishes_PartID",
                table: "BPFCEstablishes");

            migrationBuilder.DropColumn(
                name: "PartID",
                table: "BPFCEstablishes");

            migrationBuilder.AddColumn<int>(
                name: "ProcessID",
                table: "Parts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcessID",
                table: "Parts");

            migrationBuilder.AddColumn<int>(
                name: "PartID",
                table: "BPFCEstablishes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BPFCEstablishes_PartID",
                table: "BPFCEstablishes",
                column: "PartID");

            migrationBuilder.AddForeignKey(
                name: "FK_BPFCEstablishes_Parts_PartID",
                table: "BPFCEstablishes",
                column: "PartID",
                principalTable: "Parts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
