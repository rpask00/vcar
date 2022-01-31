using Microsoft.EntityFrameworkCore.Migrations;

namespace vcar.Migrations
{
    public partial class CarTableModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "mileage",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "ContactPhone",
                table: "Cars",
                newName: "Email");

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Features",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "registered",
                table: "Cars",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Features_CarId",
                table: "Features",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Features_Cars_CarId",
                table: "Features",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Features_Cars_CarId",
                table: "Features");

            migrationBuilder.DropIndex(
                name: "IX_Features_CarId",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "ContactName",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "registered",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Cars",
                newName: "ContactPhone");

            migrationBuilder.AddColumn<int>(
                name: "mileage",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
