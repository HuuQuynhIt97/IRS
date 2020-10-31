using Microsoft.EntityFrameworkCore.Migrations;

namespace INK_API.Migrations
{
    public partial class Ver134UpdateTablesleo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupplierID",
                table: "Processes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InkTblObjectID",
                table: "Parts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_ProcessID",
                table: "Supplier",
                column: "ProcessID");

            migrationBuilder.CreateIndex(
                name: "IX_Processes_SupplierID",
                table: "Processes",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_InkTblObjectID",
                table: "Parts",
                column: "InkTblObjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_InkTblObjects_InkTblObjectID",
                table: "Parts",
                column: "InkTblObjectID",
                principalTable: "InkTblObjects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_Supplier_SupplierID",
                table: "Processes",
                column: "SupplierID",
                principalTable: "Supplier",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parts_InkTblObjects_InkTblObjectID",
                table: "Parts");

            migrationBuilder.DropForeignKey(
                name: "FK_Processes_Supplier_SupplierID",
                table: "Processes");

            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_Processes_ProcessID",
                table: "Supplier");

            migrationBuilder.DropIndex(
                name: "IX_Supplier_ProcessID",
                table: "Supplier");

            migrationBuilder.DropIndex(
                name: "IX_Processes_SupplierID",
                table: "Processes");

            migrationBuilder.DropIndex(
                name: "IX_Parts_InkTblObjectID",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "SupplierID",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "InkTblObjectID",
                table: "Parts");
        }
    }
}
