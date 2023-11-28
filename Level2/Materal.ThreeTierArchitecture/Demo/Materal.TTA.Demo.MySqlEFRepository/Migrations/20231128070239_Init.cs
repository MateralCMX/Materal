using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Materal.TTA.Demo.MySqlEFRepository.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TestDomain",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "char(36)", nullable: false),
                    StringType = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    IntType = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    ByteType = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    DecimalType = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EnumType = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    DateTimeType = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestDomain", x => x.ID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestDomain");
        }
    }
}
