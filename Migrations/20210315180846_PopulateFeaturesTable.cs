using Microsoft.EntityFrameworkCore.Migrations;

namespace vcar.Migrations
{
    public partial class PopulateFeaturesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES('Airbag')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES('Leather seats')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES('Heated seats')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES('Bluetooth')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES('Bluetooth')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES('Blind spot monitoring')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES('Backup camera')");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Features");
        }
    }
}

