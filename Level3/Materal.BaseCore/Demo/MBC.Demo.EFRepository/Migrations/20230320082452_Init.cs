using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MBC.Demo.EFRepository.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuAuthority",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false, comment: "名称"),
                    Code = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false, comment: "代码"),
                    Path = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true, comment: "地址"),
                    Index = table.Column<int>(type: "INTEGER", nullable: false, comment: "位序"),
                    ParentID = table.Column<Guid>(type: "TEXT", nullable: true, comment: "父级"),
                    SubSystemID = table.Column<Guid>(type: "TEXT", nullable: false, comment: "所属子系统唯一标识"),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuAuthority", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Account = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuAuthority");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
