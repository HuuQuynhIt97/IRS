using Microsoft.EntityFrameworkCore.Migrations;

namespace INK_API.Migrations
{
    public partial class Ver110AddScheduleTablesleo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PartID",
                table: "BPFCEstablishes",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
