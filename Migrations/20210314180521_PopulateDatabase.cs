using Microsoft.EntityFrameworkCore.Migrations;

namespace vcar.Migrations
{
    public partial class PopulateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES('BMW')");
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES('Audi')");
            migrationBuilder.Sql("INSERT INTO Makes (Name) VALUES('Toyota')");

            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES('Corolla',(SELECT Id from Makes WHERE Name ='Toyota'))");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES('Supra',(SELECT Id from Makes WHERE Name ='Toyota'))");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES('Yaris',(SELECT Id from Makes WHERE Name ='Toyota'))");

            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES('X3',(SELECT Id from Makes WHERE Name ='BMW'))");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES('X1',(SELECT Id from Makes WHERE Name ='BMW'))");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES('X5',(SELECT Id from Makes WHERE Name ='BMW'))");

            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES('A3',(SELECT Id from Makes WHERE Name ='Audi'))");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES('R6',(SELECT Id from Makes WHERE Name ='Audi'))");
            migrationBuilder.Sql("INSERT INTO Models (Name,MakeId) VALUES('S3',(SELECT Id from Makes WHERE Name ='Audi'))");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Makes");
        }
    }
}

