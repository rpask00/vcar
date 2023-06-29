using Microsoft.EntityFrameworkCore.Migrations;

namespace vcar.Migrations
{
    public partial class RenameLastUpdateProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "date",
                table: "Cars",
                newName: "lasUpdate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lasUpdate",
                table: "Cars",
                newName: "date");
        }
    }
}

