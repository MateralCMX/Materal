using Microsoft.EntityFrameworkCore.Migrations;

namespace Deploy.SqliteEFRepository.Migrations
{
    public partial class AddStopParamsAndOtherParams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OtherParams",
                table: "ApplicationInfo",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StopParams",
                table: "ApplicationInfo",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtherParams",
                table: "ApplicationInfo");

            migrationBuilder.DropColumn(
                name: "StopParams",
                table: "ApplicationInfo");
        }
    }
}
