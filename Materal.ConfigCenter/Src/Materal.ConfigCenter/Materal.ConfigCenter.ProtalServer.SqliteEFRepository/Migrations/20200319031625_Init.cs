using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Materal.ConfigCenter.ProtalServer.SqliteEFRepository.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Namespace",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(nullable: false),
                    ProjectID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Namespace", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    Account = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });
            migrationBuilder.Sql(@"
Insert into [User]
Values('0D1CAF91-EAF1-4B9E-B1C2-B257FC3D3674','0001-01-01 00:00:00','0001-01-01 00:00:00', 'Admin', '39F40B095A4C6EC0B0C8B557901740CB')
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Namespace");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
