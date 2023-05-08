using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Materal.Oscillator.DRSqlServerEFRepository.Migrations
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
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScheduleData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScheduleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WorkResults = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthenticationCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
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
