using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RC.ServerCenter.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Namespace",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, comment: "名称"),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false, comment: "描述"),
                    ProjectID = table.Column<Guid>(type: "TEXT", nullable: false, comment: "命名空间唯一标识"),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Namespace", x => x.ID);
                },
                comment: "命名空间");

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, comment: "名称"),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false, comment: "描述"),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ID);
                },
                comment: "项目");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Namespace");

            migrationBuilder.DropTable(
                name: "Project");
        }
    }
}
