using Microsoft.EntityFrameworkCore.Migrations;

namespace INK_API.Migrations
{
    public partial class Ver122UpdateTablesleo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InkTblObjectID",
                table: "Schedules",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProcessID",
                table: "Schedules",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ArticleNoID",
                table: "Schedules",
                column: "ArticleNoID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_InkTblObjectID",
                table: "Schedules",
                column: "InkTblObjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ModelNameID",
                table: "Schedules",
                column: "ModelNameID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ModelNoID",
                table: "Schedules",
                column: "ModelNoID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ProcessID",
                table: "Schedules",
                column: "ProcessID");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_ArticleNos_ArticleNoID",
                table: "Schedules",
                column: "ArticleNoID",
                principalTable: "ArticleNos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_InkTblObjects_InkTblObjectID",
                table: "Schedules",
                column: "InkTblObjectID",
                principalTable: "InkTblObjects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_ModelNames_ModelNameID",
                table: "Schedules",
                column: "ModelNameID",
                principalTable: "ModelNames",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_ModelNos_ModelNoID",
                table: "Schedules",
                column: "ModelNoID",
                principalTable: "ModelNos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Schedules_ArticleNos_ArticleNoID",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_InkTblObjects_InkTblObjectID",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_ModelNames_ModelNameID",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_ModelNos_ModelNoID",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Processes_ProcessID",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_ArticleNoID",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_InkTblObjectID",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_ModelNameID",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_ModelNoID",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_ProcessID",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "InkTblObjectID",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "ProcessID",
                table: "Schedules");
        }
    }
}
