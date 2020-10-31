using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace INK_API.Migrations
{
    public partial class Ver156AddChemicalPartTablesleo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SchedulesUpdates",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelName = table.Column<string>(nullable: true),
                    ModelNo = table.Column<string>(nullable: true),
                    ArticleNo = table.Column<string>(nullable: true),
                    Treatment = table.Column<string>(nullable: true),
                    Process = table.Column<string>(nullable: true),
                    ApprovalStatus = table.Column<bool>(nullable: false),
                    FinishedStatus = table.Column<bool>(nullable: false),
                    ApprovalBy = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    Season = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ProductionDate = table.Column<DateTime>(nullable: true),
                    EstablishDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedulesUpdates", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchedulesUpdates");
        }
    }
}
