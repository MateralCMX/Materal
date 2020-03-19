using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Materal.ConfigCenter.ConfigServer.SqliteEFRepository.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfigurationItem",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    ProjectID = table.Column<Guid>(nullable: false),
                    NamespaceID = table.Column<Guid>(nullable: false),
                    ProjectName = table.Column<string>(maxLength: 100, nullable: false),
                    NamespaceName = table.Column<string>(maxLength: 100, nullable: false),
                    Key = table.Column<string>(maxLength: 100, nullable: false),
                    Value = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationItem", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfigurationItem");
        }
    }
}
