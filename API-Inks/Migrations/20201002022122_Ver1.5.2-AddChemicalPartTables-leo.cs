using Microsoft.EntityFrameworkCore.Migrations;

namespace INK_API.Migrations
{
    public partial class Ver152AddChemicalPartTablesleo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_InkTblObjects_InkTblObjectID",
                table: "Parts");

            // migrationBuilder.DropIndex(
            //     name: "IX_Parts_InkTblObjectID",
            //     table: "Parts");

            migrationBuilder.DropColumn(
                name: "InkTblObjectID",
                table: "Parts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InkTblObjectID",
                table: "Parts",
                type: "int",
                nullable: true);

            // migrationBuilder.CreateIndex(
            //     name: "IX_Parts_InkTblObjectID",
            //     table: "Parts",
            //     column: "InkTblObjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_InkTblObjects_InkTblObjectID",
                table: "Parts",
                column: "InkTblObjectID",
                principalTable: "InkTblObjects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
