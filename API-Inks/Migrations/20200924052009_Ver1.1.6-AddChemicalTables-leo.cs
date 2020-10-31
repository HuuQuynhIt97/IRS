using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace INK_API.Migrations
{
    public partial class Ver116AddChemicalTablesleo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chemicals",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<string>(nullable: true),
                    ManufacturingDate = table.Column<DateTime>(nullable: false),
                    MaterialNO = table.Column<string>(nullable: true),
                    Process = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(nullable: true),
                    SupplierID = table.Column<int>(nullable: false),
                    VOC = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<int>(nullable: false),
                    ExpiredTime = table.Column<int>(nullable: false),
                    DaysToExpiration = table.Column<int>(nullable: false),
                    isShow = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chemicals", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Chemicals_Supplier_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Supplier",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chemicals_SupplierID",
                table: "Chemicals",
                column: "SupplierID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chemicals");
        }
    }
}
