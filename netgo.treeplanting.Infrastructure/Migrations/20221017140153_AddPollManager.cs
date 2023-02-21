using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace netgo.treeplanting.Infrastructure.Migrations
{
    public partial class AddPollManager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PollManager",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PollManager",
                table: "User");
        }
    }
}
