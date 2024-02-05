using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RC.EnvironmentServer.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfigurationItem",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProjectID = table.Column<Guid>(type: "TEXT", nullable: false, comment: "项目唯一标识"),
                    ProjectName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, comment: "项目名称"),
                    NamespaceID = table.Column<Guid>(type: "TEXT", nullable: false, comment: "命名空间唯一标识"),
                    NamespaceName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, comment: "命名空间名称"),
                    Key = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, comment: "键"),
                    Value = table.Column<string>(type: "TEXT", nullable: false, comment: "值"),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false, comment: "描述"),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationItem", x => x.ID);
                },
                comment: "配置项");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfigurationItem");
        }
    }
}
