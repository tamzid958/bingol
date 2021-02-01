using Microsoft.EntityFrameworkCore.Migrations;

namespace Bingol.Migrations
{
    public partial class MigrationV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserPhone",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserPhone",
                table: "AspNetUsers",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "UserType",
                table: "AspNetUsers",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }
    }
}
