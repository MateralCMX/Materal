using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Materal.ApplicationUpdate.EFRepository.Migrations
{
    public partial class Initialization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationLog",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Application = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Level = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Logger = table.Column<string>(nullable: true),
                    Callsite = table.Column<string>(nullable: true),
                    Exception = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationLog", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    Account = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Token = table.Column<string>(maxLength: 32, nullable: false),
                    TokenExpireDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationLog");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
