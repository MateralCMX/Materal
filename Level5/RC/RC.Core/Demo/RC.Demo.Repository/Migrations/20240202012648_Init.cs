using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RC.Demo.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                },
                comment: "用户");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
