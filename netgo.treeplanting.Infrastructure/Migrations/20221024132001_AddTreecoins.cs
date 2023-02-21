using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace netgo.treeplanting.Infrastructure.Migrations
{
    public partial class AddTreecoins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Treecoins",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Treecoins",
                table: "User");
        }
    }
}
