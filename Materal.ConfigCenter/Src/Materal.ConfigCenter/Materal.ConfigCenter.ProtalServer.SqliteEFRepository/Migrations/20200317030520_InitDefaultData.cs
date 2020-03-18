using Microsoft.EntityFrameworkCore.Migrations;

namespace Materal.ConfigCenter.ProtalServer.SqliteEFRepository.Migrations
{
    public partial class InitDefaultData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
Insert into [User]
Values('0D1CAF91-EAF1-4B9E-B1C2-B257FC3D3674','0001-01-01 00:00:00','0001-01-01 00:00:00', 'Admin', '39F40B095A4C6EC0B0C8B557901740CB')
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Delete from [User]");
        }
    }
}
