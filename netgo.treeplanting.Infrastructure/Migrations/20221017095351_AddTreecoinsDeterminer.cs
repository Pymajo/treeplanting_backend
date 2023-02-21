using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace netgo.treeplanting.Infrastructure.Migrations
{
    public partial class AddTreecoinsDeterminer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TreecoinsDeterminer",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TreecoinsDeterminer",
                table: "User");
        }
    }
}
