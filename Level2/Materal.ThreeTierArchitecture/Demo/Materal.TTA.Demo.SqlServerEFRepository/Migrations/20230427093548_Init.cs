using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Materal.TTA.Demo.SqlServerEFRepository.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestDomain",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StringType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IntType = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    ByteType = table.Column<byte>(type: "tinyint", nullable: false),
                    DecimalType = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EnumType = table.Column<byte>(type: "tinyint", nullable: false),
                    DateTimeType = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestDomain", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestDomain");
        }
    }
}
