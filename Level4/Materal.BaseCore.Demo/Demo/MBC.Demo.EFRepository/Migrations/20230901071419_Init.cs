using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MBC.Demo.EFRepository.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyTree",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false, comment: "名称"),
                    ParentID = table.Column<Guid>(type: "TEXT", nullable: true, comment: "父级唯一标识"),
                    Index = table.Column<int>(type: "INTEGER", nullable: false, comment: "位序"),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyTree", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false, comment: "姓名"),
                    Sex = table.Column<byte>(type: "INTEGER", nullable: false, comment: "性别"),
                    Account = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, comment: "账号"),
                    Password = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false, comment: "密码"),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyTree");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
