using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace netgo.treeplanting.Infrastructure.Migrations
{
    public partial class AddTreeschoolSeedling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Treeschool",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treeschool", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seedling",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TreeSpecies = table.Column<string>(type: "varchar(25)", nullable: false),
                    TreeschoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    XCoordinate = table.Column<int>(type: "int", nullable: false),
                    YCoordinate = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seedling", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seedling_Treeschool_TreeschoolId",
                        column: x => x.TreeschoolId,
                        principalTable: "Treeschool",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seedling_TreeschoolId",
                table: "Seedling",
                column: "TreeschoolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Seedling");

            migrationBuilder.DropTable(
                name: "Treeschool");
        }
    }
}
