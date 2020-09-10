using System;
using Authority.Common;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Authority.SqliteEFRepository.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Account = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });
            var adminID = Guid.NewGuid();
            string password = AuthorityConfig.EncodePassword(AuthorityConfig.DefaultPassword);
            migrationBuilder.Sql($@"
Insert into [User]
Values('{adminID.ToString().ToUpper()}', '0001-01-01 00:00:00', '0001-01-01 00:00:00', '管理员', 'Admin', '{password}')
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
