using Microsoft.EntityFrameworkCore.Migrations;

namespace INK_API.Migrations
{
    public partial class Ver140UpdateTablesleo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "RoleUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "RoleUsers");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "RoleUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "RoleUsers");

            migrationBuilder.AddColumn<string>(
                name: "CreatedDate",
                table: "RoleUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "RoleUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
