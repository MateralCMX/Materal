using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RC.Deploy.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationInfo",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false, comment: "名称"),
                    RootPath = table.Column<string>(type: "TEXT", nullable: false, comment: "根路径"),
                    MainModule = table.Column<string>(type: "TEXT", nullable: false, comment: "主模块"),
                    ApplicationType = table.Column<byte>(type: "INTEGER", nullable: false, comment: "应用程序类型"),
                    IsIncrementalUpdating = table.Column<bool>(type: "INTEGER", nullable: false, comment: "增量更新"),
                    RunParams = table.Column<string>(type: "TEXT", nullable: true, comment: "运行参数"),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationInfo", x => x.ID);
                },
                comment: "应用程序信息");

            migrationBuilder.CreateTable(
                name: "DefaultData",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ApplicationType = table.Column<byte>(type: "INTEGER", nullable: false, comment: "应用程序类型"),
                    Key = table.Column<string>(type: "TEXT", nullable: false, comment: "键"),
                    Data = table.Column<string>(type: "TEXT", nullable: false, comment: "数据"),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultData", x => x.ID);
                },
                comment: "默认数据");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationInfo");

            migrationBuilder.DropTable(
                name: "DefaultData");
        }
    }
}
