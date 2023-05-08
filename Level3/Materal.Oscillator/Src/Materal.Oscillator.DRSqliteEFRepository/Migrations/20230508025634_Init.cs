using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Materal.Oscillator.DRSqliteEFRepository.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flow",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    JobKey = table.Column<string>(type: "TEXT", nullable: false),
                    ScheduleData = table.Column<string>(type: "TEXT", nullable: false),
                    ScheduleID = table.Column<Guid>(type: "TEXT", nullable: false),
                    WorkID = table.Column<Guid>(type: "TEXT", nullable: true),
                    WorkResults = table.Column<string>(type: "TEXT", nullable: true),
                    AuthenticationCode = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flow", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flow");
        }
    }
}
